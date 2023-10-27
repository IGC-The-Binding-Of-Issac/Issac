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
    [SerializeField] int spawnObj; //�Ӹ� ���� ����
    [SerializeField] int gap;
    [SerializeField] GameObject larryBody;
    [SerializeField] Vector3 MoveDir; //������ ���� (��, �Ʒ�, ��,  ��)
    [SerializeField] List<GameObject> segments = new List<GameObject>(); // ���� ������Ʈ�� ���� �迭
    [SerializeField] List<Vector3> PositionHistory = new List<Vector3>(); // ���� ��ġ�� �����ϴ� �迭 -> ���� ������Ʈ�� �տ� ���󰡰� 


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
        gap = 70; // ���� ������Ʈ�� ���̿� ������ �����Ҷ� : ������ ������ �پ ������

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
        // Snake ��ü�� segments ����Ʈ�� ����
        segments.Add(larryBody);

        // Snake�� �Ѿƴٴϴ� ����(segment ������Ʈ)�� �����ϰ�, segments ����Ʈ�� ����
        for (int i = 0; i < spawnObj-1; ++i)
        {
            AddSegment();
        }
    }

    private void AddSegment()
    {
        GameObject segment = Instantiate(larryBody);
        segment.transform.position = segments[segments.Count - 1].transform.position;
        segments.Add(segment);
    }

    //������� ������
    private void MoveSegment() 
    {
        //����(�Ӹ�)������
        transform.position += MoveDir * Time.deltaTime * moveSpeed;

        //���� ������
        PositionHistory.Insert(0, transform.position); //��ġ ��Ƴ��� �迭-> 0��° index�� �Ӹ���ġ ���
        int index = 0;
        foreach (var body in segments)
        {
            Vector3 posi = PositionHistory[Mathf.Min(index * gap, PositionHistory.Count - 1)];
            // index : �Ӹ��� �����̰� �󸶵ڿ� (gap * 0)��ŭ ������
            // gap : ���� ������Ʈ ���̵� ���� ������?
            Vector3 posiforwad = posi - body.transform.position;
            body.transform.position += posiforwad;
            index++;
        }

    }

    // ���� �ð� ����, ���� ���·� ��ȯ
    void randNum() 
    {
        // state 1.up , 2. down , 3.left , 4. right ���� �߿� �ϳ��� �������� ��
        int rand = 0;
        rand = Random.Range(1, 5); // 1~4��
        stateNum = rand;

        // �¿� ������ ��ȯ x
        // ���Ʒ� ������ ��ȯ x
        /*
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
        */
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
