using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public enum TENEMY_STATE // ��ũ��Ʈ�� ���� ���� 
{
    Idle,
    Prowl,
    Tracking,
    Shoot,
    Die
}

public class TEnemy : MonoBehaviour
{
    /// <summary>
    /// - ���� § Zombie_FSM �� ����� �ϴ�, �ӽ��� ���ư��� ��ü 
    /// - Enemy ��ũ��Ʈ�� �� ����
    /// </summary>

    // TEnemy�� ���� �ӽ�
    public TEnemy_HeadMachine<TEnemy> headState;

    /// <summary>
    /// 1. ���� �̸� ������ ���� state�迭
    /// 2. System.Enum.GetValues(typeof(state�̸�)).Length : enum Ÿ���� ���� ���ϱ� ,  ���⼭�� 5
    /// </summary>
    public TEnemy_FSM<TEnemy>[] arrayState = new TEnemy_FSM<TEnemy>[System.Enum.GetValues(typeof(TENEMY_STATE)).Length];

    // ��� ���� Enemy�� ����
    [SerializeField] protected bool isTracking;              // tracking �ϴ°�?
    [SerializeField] protected bool isProwl;                 // prowl (��ȸ) �ϴ°�?
    [SerializeField] protected bool isDetective;             // detective (����) �ϴ°�?
    [SerializeField] protected bool isShoot;                 // shoot (�� ��) �ϴ°�?

    // enemy ���� ������Ƽ
    public bool getIsTracking { get => isTracking; }
    public bool getisProwl { get => isProwl; }
    public bool getisDetective { get => isDetective; }
    public bool getisShoot { get => isShoot; }


    // Enemy�� ������ ���� �⺻ ���ȵ�
    public bool playerInRoom;                   // �÷��̾ �濡 ���Դ��� ����
    protected float hp;                         // hp
    protected float sight;                      // �þ� ����
    protected float moveSpeed;                  // ������ �ӵ�
    protected float waitforSecond;              // destroy �� ��� �ð� 
    protected float attaackSpeed;               // ���� �ӵ�
    protected float bulletSpeed;                // �Ѿ� �ӵ� 
    protected float fTime;                      // prowl - ���� �̵� �ð�
    protected float randRange;                  // prowl - ���� �̵� �Ÿ�
    protected bool isRaadyShoot;                // shoot -  �� ��� ����
    protected bool isFlipped = true;                   // ������

    //
    protected bool knockBackState = false;      // �˹� 
    protected float mx;                         // ���� x
    protected float my;                         // ���� y
    protected float xRan;                       // x ���� ������
    protected float yRan;                       // y ���� ������

    //
    protected string dieParameter;              // �״� �ִϸ��̼� ���� �Ķ����
    protected string shootParameter;            // �ѽ�� �ִϸ��̼� ���� �Ķ����

    // enemy ���� ������Ƽ
    public float getMoveSpeed { get => moveSpeed; }
    public float getSight { get => sight; }
    public bool setIsReadyShoot
    {
        set { isRaadyShoot = value; }
    }

    // Enemy�� Hp��
    protected float maxhp;                      // hp ���� max 
    public Image hpBarSlider;                   // hp ���� �̹���

    // ������Ʈ
    public GameObject enemyBullet;              // �Ѿ�
    public GameObject roomInfo;                 // �� ���� ������Ʈ
    public Transform playerPosi;             // �÷��̾� ��ġ

    // Enemy�� ������ �ִ� ������Ʈ (init ���� �ʱ�ȭ)
    protected Animator animator;                // �ִϸ�����

    // Enemy�� ���� (enum)
    public TENEMY_STATE eCurState;              // ���� ����
    public TENEMY_STATE ePreState;              // ���� ����

    /// <summary>
    /// 1. setStateArray()
    ///     - ���� �迭 arrayState�� ���� 
    ///     - ���� �迭�� this�� �־ ���� ���Ͱ� ��
    ///     - ���� ���Ϳ��� ���� ���� (idle)�� �����ϸ�, ���º�ȭ�� �� ��
    /// 2. En_setState()
    ///     - ���Ͱ� �ʿ��� ����
    ///     - ex) playerInRoom , hp, sight��
    /// 3, En_kindOfEnemy()
    ///     - � �ൿ�� �ϴ��� 
    ///     - isTracking(����) , isProwl(��ȸ) ,isDetective(����) �� true, false�� ǥ��
    /// 4. En_Start()
    ///     - ���� ���¸� ���� ���ִ� �Լ�
    ///     - ���� ������ start�� �������
    ///     
    /// </summary>


    protected void En_stateArray()
    {
        init();
    }

    // �ʿ��� ������ �ʱ�ȭ
    private void init()
    {
        // Enemy_HeadMachine�� Ÿ���� TEnemy�� ���� , �ӽ��� ���� (new ���)
        headState = new TEnemy_HeadMachine<TEnemy>();

        /// <summary>
        /// 1. �ش� ���� (��ũ��Ʈ)�� �����ڸ� ��� -> Enemy �� �پ��ִ� ��ü�� �Ѱ��� 
        /// 2. new ��ũ��Ʈ �̸� (�Ű�����) 
        /// 3. Enemy�� ��� �ϴ� �������� ���� Enemy Ÿ��(Enemy�� ��� �ް� �ֱ� ����)
        ///     <!�׽�Ʈ �ʿ�>
        ///     -> ���� ���Ϳ��� init�� �����ϸ� ���¸ӽ��� ���� ���� ������?
        ///  
        /// Q. �� Enemy_Idle���� ��ũ��Ʈ�� FSM<TENemy> �迭�� ���� �� �ִ°�?
        ///     A. FSM<Zombie_FSM>�� ���� ��ũ��Ʈ���� FSM<Zombie_FSM>�� arr�� �ֱ⶧�� 
        /// Q. �� ��ư�  arrayState[(int)TENEMY_STATE.Idle]�̷��� ����? arr[0]�ϸ� �ȵ�?
        ///     A. �������� ���ؼ� , arr[0] �ϸ� �迭�� 0��°�� � Ÿ������ �𸣴ϱ�
        /// </summary>
        arrayState[(int)TENEMY_STATE.Idle] = new Enemy_Idle(this);
        arrayState[(int)TENEMY_STATE.Prowl] = new Enemy_Prowl(this);
        arrayState[(int)TENEMY_STATE.Tracking] = new Enemy_Tracking(this);
        arrayState[(int)TENEMY_STATE.Shoot] = new Enemy_Shoot(this);
        arrayState[(int)TENEMY_STATE.Die] = new Enemy_Die(this);
        //Debug.Log(this.gameObject.tag); -> �ڽ� ������Ʈ���� �����ϸ� �ڽ� ������Ʈ�� this

        // Enemy ���¸� Idle ���·� �ʱ� ����
        headState.Setstate(arrayState[(int)TENEMY_STATE.Idle], this);

        // ������Ʈ �ʱ�ȭ
        animator = this.gameObject.GetComponent<Animator>();
    }

    public virtual void En_setState() { }         // �ʱ� ���� (Getcomponent, hp ���� ��)
    public virtual void En_kindOfEnemy() { }      // Enemy�� ���� (isTracking , isProwl ,isDetective )
    public void En_Start()                        // ���� ���� start���� ����         
    {
        En_stateArray();                          // �ʱ� �迭 ����
        E_Enter();                                // ���� ���� (idle)�� begin ���� 
    }

    public void E_Enter()
    {

        /// <summary>
        /// - init()�޼��忡�� ���� ���¸� idle �� ����
        /// - idle ������ Enter �޼��� ����
        /// </summary>
        headState.H_Enter();
    }
    public void E_Excute()
    {
        headState.H_Excute();
    }

    public void E_Exit()
    {
        headState.H_Exit(); ;
    }




    /// <summary>
    /// - ���� ��ȯ
    /// 1. ���� �ȿ��� (���� ��ũ��Ʈ �ȿ���) "������-> ��������" ��ȯ �� �� ���
    /// </summary>
    public void ChageFSM(TENEMY_STATE ps)
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(TENEMY_STATE)).Length; i++)
        {
            if (i == (int)ps)
                headState.Chage(arrayState[(int)ps]);
        }

    }

    /// <summary>
    /// * ��Ÿ ���� �޼���
    /// </summary>  

    // �÷��̾� �Ĵٺ���
    public void e_Lookplayer()
    {
        if (transform.position.x > playerPosi.position.x && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPosi.position.x && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    // tracking�� enemy �� Ž��
    public void e_findPlayer()                             
    {
        playerPosi = GameObject.FindWithTag("Player").transform;
    }
    // tracking ������
    public void e_Tracking()                               
    {
        if (knockBackState) // �˹� ����
            return;

        gameObject.transform.position
            = Vector3.MoveTowards(gameObject.transform.position, playerPosi.transform.position, moveSpeed * Time.deltaTime);
    }

    // ���� �ȿ� player ����
    public bool e_SearchingPlayer()                         
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 sightSize = new Vector2(x, y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sightSize, sight); //���� ��ġ , ����

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    // enemy �ʱ� ��ġ
    public void e_moveInialize() 
    {
        mx = gameObject.transform.position.x;
        my = gameObject.transform.position.y;
        xRan = mx;
        yRan = my;
    } 

    //���� ������
    public void e_Prwol()
    {
        Vector3 moveRan = new Vector3(xRan, yRan, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, moveSpeed * Time.deltaTime);
    }

    // �� ���
    public void e_Shoot() 
    {
        animator.SetTrigger(shootParameter);
        if (isRaadyShoot) {
            GameObject bulletobj = Instantiate(enemyBullet, transform.position + new Vector3(0, -0.1f, 0), Quaternion.identity);
            isRaadyShoot = false;
        }
    }

    // Tracking -> Shoot���� �Ѿ�� ���� (invoke)
    public void involeShoot()
    {
        Invoke("chageToShoot", attaackSpeed);             // 3���Ŀ�  

    }
    public void chageToShoot()
    {
        ChageFSM(TENEMY_STATE.Shoot);           // Shoot���� ���� ��ȭ
    }

    // ������ ������ �ڷ�ƾ ����
    public void startRaomPosiCoru()
    {
        StartCoroutine(checkPosi());
    }
    //���� ������ �˻�
    public IEnumerator checkPosi()
    {
        while (true)
        {
            yield return new WaitForSeconds(fTime);
            xRan = Random.Range(mx + randRange, mx - randRange);    // x��ġ�� ���� ��ġ randRange���� , ������ġ -randRange����
            yRan = Random.Range(my + randRange, my - randRange);    // y��ġ ����
        }
    }


    // �Ϲ� ���� collider�˻�
    private void OnCollisionStay2D(Collision2D collision)       
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();                 //�÷��̾�� �ε����� �÷��̾��� hp����
        }
    }

    // �Ϲ� ���� trigger �˻�
    private void OnTriggerEnter2D(Collider2D collision)             
    {
        if (collision.gameObject.CompareTag("Tears"))               //�����̶� �ε����� �� ��ȭ
        {
            Color oriColor = gameObject.GetComponent<SpriteRenderer>().color;
            StartCoroutine(Hit(oriColor));
        }
    }

    // �� ��ȭ �ڷ�ƾ
    IEnumerator Hit(Color oriColor)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = oriColor;
    }

    // �˹� �ڷ�ƾ 
    public IEnumerator knockBack()
    {
        knockBackState = true;
        yield return new WaitForSeconds(0.2f);
        knockBackState = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    // �Ϲ� ���� hp ��
    public void CheckHp()
    {
        hpBarSlider.fillAmount = hp / maxhp;
    }

    // player ������ ��ŭ �� ���� (Tear ��ũ��Ʈ ���� ��� )
    public void GetDamage(float damage) 
    {
        hp -= damage;
        CheckHp();
    }

    // enemy ����
    public bool e_isDead() 
    {
        if (hp <= 0)
            return true;
        return false;
    }

    // enemy ����
    public void e_destroyEnemy()
    {
        animator.SetBool(dieParameter , true);
        Destroy(gameObject , waitforSecond);
    }
}
