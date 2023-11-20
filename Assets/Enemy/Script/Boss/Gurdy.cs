using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Gurdy : TEnemy
{
    /// <summary>
    /// 
    /// 1. ������ ����
    /// 2. �¿쿡�� ����ź ��
    /// 3. �տ��� 4���� �Ѿ� ��
    /// 4. �տ� pooter �θ��� ��ȯ
    /// 
    /// </summary>
    /// 
    [Header("BigAttackFly")]
    [SerializeField] GameObject bigShootBullet;
    [SerializeField] GameObject rightPosition;
    [SerializeField] Animator childAni;
    public Transform[] Children;

    [SerializeField] float  stateTime;            
    [SerializeField] int    stateNum;
    [SerializeField] float  currTime;                // ���� ������ �ð�
    [SerializeField] bool chageState;               // ���º�ȯ
    bool coruState;
    Coroutine runningCoroutine = null;

    void Start()
    {
        // ������ , �ڽ� �迭 �޾ƿ���
        // index0 : ����
        // index1 : �Ӹ�
        // index2 : right
        // index3 : down
        // index4 : left
        // index5 : top

        Children = gameObject.GetComponentsInChildren<Transform>();
        animator = GetComponent<Animator>();
        childAni = Children[1].gameObject.GetComponent<Animator>();

        playerInRoom        = false;
        dieParameter        = "isBigFlyDie";

        // Enemy
        hp                  = 100f;
        waitforSecond       = 0.4f;
        attaackSpeed        = 1.5f; // �Ѿ� �߻� �ϴ� �ð� 
        bulletSpeed         = 5f;

        maxhp               = hp;

        //Gurdy
        randTime();

        currTime = stateTime;
        chageState = true;
        coruState = true;
    }

    private void Update()
    {
        if (playerInRoom) 
        {
            Move();
        }
    }

    void Move() 
    {

        currTime -= Time.deltaTime;
        if (currTime > 0)
        {

        }
        else if (currTime <= 0) 
        {
            if (runningCoroutine != null) //�������� �ڷ�ƾ�� ������
            {
                // ���߱�
                StopCoroutine(runningCoroutine);
            }

            // �ʱ�ȭ
            randNum();
            randTime();
            currTime = stateTime;

            if (stateNum == 1)
                gurdyShoot();
            else if (stateNum == 2)
                gurdyGeneFly();

            coruState = true;
        }

    }

    void gurdyShoot() 
    {
        Debug.Log("�ŵ� �ѽ�");


        int rand = Random.Range(2 , 6); // 2~5

        if (coruState)
        {
            runningCoroutine = StartCoroutine(ShootBullets(Children[rand].gameObject));
            coruState = false;
        }


    }

    IEnumerator ShootBullets(GameObject _obj)
    {
        float randGravityScale;


        while (true)
        {
            float randWait = Random.Range(0.2f, 0.5f);
            bool isbullet = true;
            if (isbullet)
            {

                GameObject bu = Instantiate(bigShootBullet, _obj.transform.position, transform.rotation);
                bu.GetComponent<Rigidbody2D>().velocity = _obj.transform.right * bulletSpeed;

                randGravityScale = Random.Range(0f, 1f);
                bu.GetComponent<Rigidbody2D>().gravityScale += randGravityScale;
                isbullet = false;
            }
            yield return new WaitForSeconds(randWait);
        }
    }

    void gurdyGeneFly() 
    {
        Debug.Log("�ŵ� ����");

    }

    void randTime()
    {
        //1f ~ 10f ���̿��� �ð�
        stateTime = Random.Range(1f, 3f);
    }

    void randNum()
    {
        stateNum = Random.Range(1, 3); // 1~2��
    }


}
