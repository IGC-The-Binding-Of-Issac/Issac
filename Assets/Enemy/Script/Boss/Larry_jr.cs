using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/*
public enum LarryState 
{
    // ��, �Ʒ�, ��, �� ���� �����̴� ����
    // 0���� 4������ state 
    Idle, Up, Down, Left, Right
}
*/

public class Larry_jr : Enemy
{
    [Header("���� ��")]
    // [SerializeField] LarryState larryState;
    [SerializeField] int stateNum; //���� ������ ��ȣ
    [SerializeField] float stateTime; //���� ������ �ð�

    // ���º�ȯ
    [SerializeField] float currTime; //���� ������ �ð�
    [SerializeField] bool chageState;

    [Header("����")]
    [SerializeField] Transform larryBody;
    [SerializeField] int spawnObj; //�Ӹ� ���� ����
    [SerializeField] List<Transform> segments = new List<Transform>();
    [SerializeField] Vector3 MoveDir; //������ ���� (��, �Ʒ�, ��,  ��)

    void Start()
    {


        playerInRoom = false;
        dieParameter = "isDie";

        // Enemy
        hp = 50f;
        sight = 5f;
        moveSpeed = 2f;
        waitforSecond = 0.5f;

        maxhp = hp;

        //Larry
        stateNum = 0;
        stateTime = 3f;
        chageState = true;

        randTime(); //�ʱ⿡ ������ �ð� �ѹ� ���س���

        currTime = stateTime; // �ʱ⿡ ���� �ð�
        spawnObj = 13; //�Ӹ� ���� �ʱ� ���� ����

        Setup(); //larry�� �� �����
    }

    private void Update()
    {
        if (playerInRoom)
            Move();
    }

    override public void Move()
    {
        // ������
        MoveSegment();

        //���� ���ϱ�
        currTime -= Time.deltaTime;
        if (currTime >= 0 && chageState)
        {
            randNum(); //���� ���� ���ϱ� (1~4)
            if (stateNum == 1)
                MoveDir = Vector3.up;
            else if (stateNum == 2)
                MoveDir = Vector3.down;
            else if (stateNum == 3)
                MoveDir = Vector3.left;
            else if (stateNum == 4)
                MoveDir = Vector3.right;
            chageState = false;
        }
        else if (currTime <= 0) 
        {
            randTime(); // ���� Ÿ�� ���ϱ�
            currTime = stateTime; //�ð� �ʱ�ȭ
            chageState = true;
        }
        
    }

    // �Ӹ� + ���� �����
    private void Setup()
    {
        // �Ӹ�(����)�� segment ����Ʈ�� ����
        segments.Add(transform);

        // �Ӹ��� ���� �ٴϴ� ���� (larryBody)�� ����, segment ����Ʈ�� �����ϱ�
        for (int i = 1; i < spawnObj; i++) 
        {
            AddSegment();
        }

    }

    // �Ӹ��� ���� ���̱�
    private void AddSegment() 
    {
        // position ����ؼ� ����Ȱ� �ڿ� ����ǰ�
        Transform seg = Instantiate(larryBody);
        seg.position += segments[segments.Count - 1].position; 
        // set��ġ , ���� �ִ� ���� ��ġ + x��ġ 1��ŭ
        segments.Add(seg);
    }

    //������� ������
    private void MoveSegment() 
    {
        //����(�Ӹ�)������
        transform.position += MoveDir * Time.deltaTime * moveSpeed;
        Vector3 desti;
        desti = new Vector3(transform.position.x, transform.position.y, 0);

        //���� ������
        for (int i = 1; i < segments.Count; i++)
        {
            //���� tramsfom ����
            Vector3 now = new Vector3(segments[i].position.x, segments[i].position.y, 0);
            //���� ��ġ�� ���� �س��� desti�� �̵�
            segments[i].position = Vector3.MoveTowards(segments[i].transform.position, desti, moveSpeed * Time.deltaTime);
            //desti�� ���� ��ġ�� ����
            desti = now;
        }

    }

    // ���� �ð� ����, ���� ���·� ��ȯ
    void randNum() 
    {
        // state 1.up , 2. down , 3.left , 4. right ���� �߿� �ϳ��� �������� ��
        int rand = 0;
        // �¿� ������ ��ȯ x
        // ���Ʒ� ������ ��ȯ x
        if (stateNum == 1 || stateNum == 2) 
        {
            rand = Random.Range(3,5); // 3~4��
            stateNum = rand;
        }
        else if (stateNum == 3 || stateNum == 4)
        {
            rand = Random.Range(1, 3); // 1~2��
            stateNum = rand;
        }
        else 
        {
            rand = Random.Range(1, 3); // 1~4��
            stateNum = rand;
        }
    }
    void randTime() 
    {
        //1f ~ 10f ���̿��� �ð�
        stateTime = Random.Range(1f, 3f);
    }
    
    //���ѻ��¸ӽ� ����
    /*
    //���� ��ȯ
    private void ChageState(LarryState newState) 
    {
        StopCoroutine(larryState.ToString());
        larryState = newState;
        StartCoroutine(larryState.ToString());
    }


    // Up ���� �� ��,
    private IEnumerator Up()
    {
        // �� �ѹ� ����
        Debug.Log("���� �� �� ���� ���ϴ�");
        MoveDir = Vector3.up;

        // ���� ��ȯ �� ���� ����
        while (true) 
        {
            //transform.position += new Vector3(0 , 1, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Down()
    {
        // �� �ѹ� ����
        Debug.Log("���� �� �� �Ʒ��� ���ϴ�");
        MoveDir = Vector3.down;

        // ���� ��ȯ �� ���� ����
        while (true)
        {
            //transform.position += new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }
    private IEnumerator Right()
    {
        // �� �ѹ� ����
        Debug.Log("���� �� �� ������ ���ϴ�");
        MoveDir = Vector3.right;

        // ���� ��ȯ �� ���� ����
        while (true)
        {
            //transform.position += new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Left()
    {
        // �� �ѹ� ����
        Debug.Log("���� �� �� �������� ���ϴ�");
        MoveDir = Vector3.left;

        // ���� ��ȯ �� ���� ����
        while (true)
        {
            //transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }
    */
}
