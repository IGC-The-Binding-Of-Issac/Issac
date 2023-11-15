using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // enemy ���� ������Ƽ
    public bool getIsTracking { get => isTracking; }
    public bool getisProwl { get => isProwl; }
    public bool getisDetective { get => isDetective; }


    // Enemy�� ������ ���� �⺻ ���ȵ�
    public bool playerInRoom;               // �÷��̾ �濡 ���Դ��� ����
    protected float hp;                       // hp
    protected float sight;                  // �þ� ����
    protected float moveSpeed;              // ������ �ӵ�

    // enemy ���� ������Ƽ
    public float getMoveSpeed { get => moveSpeed; }
    public float getSight { get => sight; }

    // �÷��̾� ��ġ
    public Transform trackingTarget;

    // Enemy�� ������ �ִ� ������Ʈ (init ���� �ʱ�ȭ)
    protected Animator animator;            // �ִϸ�����

    // Enemy�� ���� (enum)
    public TENEMY_STATE eCurState;          // ���� ����
    public TENEMY_STATE ePreState;          // ���� ����

    /// <summary>
    /// - ������
    /// 1. �ڽ� Ŭ������ �θ� Ŭ������ �����ڸ� �����;��� -> ������, start�� awake �� ��������
    /// (�ذ� : �ʱ�ȭ �ϴ� �޼��带 ���� ������Ʈ�� start �Լ����� �����Ŵ)
    /// </summary>
    /*
    public TENemy() 
    {
        Debug.Log("�θ� ������ ����");
        init();
    }
    */

    /// <summary>
    /// 
    /// - ���� ���Ͱ� �ݵ�� ������ �� ��
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
    public void En_Start()                        // ���� ���� (idle)�� begin ���� 
    {
        E_Enter();
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
    /// 
    /// </summary>
    /// 
    public void e_findPlayer()                              // tracking�� enemy �� Ž��
    {
        trackingTarget = GameObject.FindWithTag("Player").transform;
    }
    public void e_Tracking()                                // tracking ������
    {
        gameObject.transform.position
            = Vector3.MoveTowards(gameObject.transform.position, trackingTarget.transform.position, getMoveSpeed * Time.deltaTime);
    }

    public bool e_SearchingPlayer()                         // ���� �ȿ� player ����
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, getSight);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                return true;
            }

        }
        return false;
    }
}
