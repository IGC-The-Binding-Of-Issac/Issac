using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LarryState 
{
    // ��, �Ʒ�, ��, �� ���� �����̴� ����
    // 0���� 4������ state 
    Idle, Up, Down, Left, Right
}

public class Larry_jr : Enemy
{
    [Header("���� ��")]
    [SerializeField] LarryState larryState;
    [SerializeField] int stateNum; //���� ������ ��ȣ
    [SerializeField] float stateTime; //���� ������ �ð�

    // ���º�ȯ
    [SerializeField] float currTime; //���� ������ �ð�
    [SerializeField] bool chageState;

    void Start()
    {
        playerInRoom = false;
        dieParameter = "isDie";

        // Enemy
        hp = 50f;
        sight = 5f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;

        maxhp = hp;

        //Larry
        stateNum = 0;
        stateTime = 3f;
        chageState = true;

        randTime(); //�ʱ⿡ ������ �ð� �ѹ� ���س���
        currTime = stateTime; // �ʱ⿡ ���� �ð�
        larryState = LarryState.Idle; // �ʱ���´� idle��
    }

    private void Update()
    {
        if (playerInRoom)
            Move();
    }

    override public void Move()
    {
        currTime -= Time.deltaTime;
        if (currTime >= 0 && chageState)
        {
            randomStateNum(); //���� ���� ���ϱ� (1~4)
            if (stateNum == 1)
                ChageState(LarryState.Up);
            else if (stateNum == 2)
                ChageState(LarryState.Down);
            else if (stateNum == 3)
                ChageState(LarryState.Left);
            else if (stateNum == 4)
                ChageState(LarryState.Right);
            chageState = false;
        }
        else if (currTime <= 0) 
        {
            randTime(); // ���� Ÿ�� ���ϱ�
            stateNum = 0; //���� ���� �ʱ�ȭ
            currTime = stateTime; //�ð� �ʱ�ȭ
            chageState = true;
        }
        
    }

    // ���� �ð� ����, ���� ���·� ��ȯ
    void randomStateNum() 
    {
        // state 1.up , 2. down , 3.left , 4. right ���� �߿� �ϳ��� �������� ��
        stateNum = Random.Range(1 , 5); // 1���� 4���� ���� 
    }
    void randTime() 
    {
        //1f ~ 10f ���̿��� �ð�
        stateTime = Random.Range(1f, 3f);
    }


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

        // ���� ��ȯ �� ���� ����
        while (true) 
        {
            Debug.Log("��");
            transform.position += new Vector3(0 , 1, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Down()
    {
        // �� �ѹ� ����
        Debug.Log("���� �� �� �Ʒ��� ���ϴ�");

        // ���� ��ȯ �� ���� ����
        while (true)
        {
            transform.position += new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed;

            yield return null;
        }
    }
    private IEnumerator Right()
    {
        // �� �ѹ� ����
        Debug.Log("���� �� �� ������ ���ϴ�");

        // ���� ��ȯ �� ���� ����
        while (true)
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

    private IEnumerator Left()
    {
        // �� �ѹ� ����
        Debug.Log("���� �� �� �������� ���ϴ�");

        // ���� ��ȯ �� ���� ����
        while (true)
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * moveSpeed;
            yield return null;
        }
    }

}
