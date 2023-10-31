using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

public class SnakeManager : Enemy
{
    /// <summary>
    /// <Larry J>
    /// 1. player�� �濡 ������ ���� ���� , isPlayerInRoom ���� �ʿ����
    /// 2. �������� FIxedUpdate���� ����
    /// 3. Head�� body���� Enemy �±� 
    /// </summary>

    [SerializeField] float distanceBetween;

    [SerializeField] List<GameObject> bodyParts = new List<GameObject>(); // Larry�� ��, ���� ������Ʈ
    [SerializeField] List<GameObject> snakeBody = new List<GameObject>();

    float countUp = 0;
    [SerializeField] Vector3 MoveDir; //������ ���� (��, �Ʒ�, ��,  ��)
    [SerializeField] int stateNum; //���� ������ ��ȣ
    [SerializeField] float stateTime; //���� ������ �ð�
    [SerializeField] float currTime; //���� ������ �ð�
    [SerializeField] bool chageState; // ���º�ȯ 

    private void Start()
    {
        //SnakeManager
        countUp = 0;
        distanceBetween = 0.2f;
        CreateBodyParts(); //�ʱ� �� ���� 

        // Enemy
        hp = 50f;
        sight = 5f;
        moveSpeed = 5f; // �̰� �ٲٸ� distanceBetween�� �ٲ㼭 ���� �ϴ� Ÿ�̹� �������!!!
        waitforSecond = 0.5f;

        maxhp = hp;

        //Snake
        stateNum = 0; //���� ��ȣ
        stateTime = 3f;
        chageState = true;

        randTime(); //�ʱ⿡ ������ �ð� �ѹ� ���س���
        currTime = stateTime; // �ʱ⿡ ���� �ð�
    }

    
    private void FixedUpdate()
    {
        if (bodyParts.Count > 0)
        {
            CreateBodyParts();
        }
        Move();
    }
    
    private void Update()
    {

    }

    // �����̴� ���� ����
    public override void Move()
    {
        SnakeMovement();

        //���� ���ϱ�
        currTime -= Time.deltaTime;
        if (currTime >= 0 && chageState)
        {
            randNum(); //���� ���� ���ϱ� (1~4)
            chageAniDir(); // �ִϸ��̼ǰ� ���� �ٲ�
            chageState = false;
        }
        else if (currTime <= 0)
        {
            randTime(); // ���� Ÿ�� ���ϱ�
            currTime = stateTime; //�ð� �ʱ�ȭ
            chageState = true;
        }
    }

    // ���¿� ���� �ִϸ��̼ǰ� ���� ��ȯ
    public void chageAniDir() 
    {
        if (stateNum == 1)
        { 
            MoveDir = Vector3.up;
            animator.SetTrigger("isLarryTop");
        }
        else if (stateNum == 2) 
        {
            MoveDir = Vector3.down;
            animator.SetTrigger("isLarryDown");
        }
        else if (stateNum == 3) 
        { 
            MoveDir = Vector3.left;
            animator.SetTrigger("isLarryLeft");
        }
        else if (stateNum == 4) 
        { 
            MoveDir = Vector3.right;
            animator.SetTrigger("isLarryRight");
        }

    }

    //���� ���� 
    void randNum()
    {
        // state 1.up , 2. down , 3.left , 4. right ���� �߿� �ϳ��� �������� ��
        int rand = 0;

        //�� ó������ ������ ����������
        if (stateNum == 0)
        {
            stateNum = 4;
        }
        else
        {
            rand = Random.Range(1, 5); // 1~4��
            stateNum = rand;
        }
    }
    //���� �ð�
    void randTime()
    {
        //1f ~ 10f ���̿��� �ð�
        stateTime = Random.Range(0.5f, 2f);
    }

    // ������
    void SnakeMovement()
    {
        //snakeBody[0]�� �Ӹ� , �Ӹ��� �������� MoveDir�������� �̵�
        snakeBody[0].transform.position += MoveDir * moveSpeed * Time.deltaTime;

        // snakeBody�� ������ ������ ( = �Ӹ��� �����ϰ� ������ ������)
        // ������ �����̰� ���� �ɾ������!
        if (snakeBody.Count > 1)
        {
            // for���� ���鼭 ù��° ����~������ �̵�
            for (int i = 1; i < snakeBody.Count; i++)
            {
                // ������ ������ MarkManager�� ������
                MarkManager markM = snakeBody[i - 1].GetComponent<MarkManager>();
                // i��° ������ ��ġ�� "������ ����"�� ��ġ
                snakeBody[i].transform.position = markM.markerList[0].position;
                // i��° ������ ȸ���� ``
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                // �ѹ� �̵� ������ list�� ������
                markM.markerList.RemoveAt(0);
            }
        }

    }

    //���� ���� �ڵ�
    void CreateBodyParts()
    {
        //�Ӹ� ���� (snakeBody�� �ƹ��͵� ���� �Ǿ����� ���� ��)
        if (snakeBody.Count == 0)
        {
            // bodyParts�� ù����(�Ӹ�) �κ���, ������ġ�� ����
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            //ù��° ��� (�Ӹ�)�� �ִϸ����� ��������
            animator = temp1.GetComponent<Animator>();

            // ������Ʈ �߰�
            // MarkManager������Ʈ , Rigidbody2D������Ʈ (Rigidbody2D�� ���� �Ⱦ����� �̸� �־���´ٰ� ��������) 
            if (!temp1.GetComponent<MarkManager>())
                temp1.AddComponent<MarkManager>();
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            // snakeBody�� instanceȭ �� ������Ʈ �߰�
            snakeBody.Add(temp1);
            // bodyParts�� ù��° ���� (ù��°�� �����ϸ鼭 �ڿ� �ִ� ������Ʈ�� �ε����� ������ / 1�� 0����, 2��1��.....)
            bodyParts.RemoveAt(0);
        }


        //snakeBody�� ���� �ٷ� �տ� (�ε��� -1)�� MarkManager�� ������
        MarkManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkManager>();
        // countUp�� �ʱ�ȭ �� �� ���� (0 �϶� ����)
        if (countUp == 0)
        {
            // ���� �տ� �ִ� (�ε��� -1) ����Ʈ�� ����
            markM.ClearmMarkerList();
        }

        // ���������� ���� , distanceBetween ����
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
        {
            // bodyParts�� ù��°�� ���� ������ ������Ʈ�� ��ġ�� ����
            GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation, transform);
            // ������Ʈ �߰�
            if (!temp.GetComponent<MarkManager>())
            {
                temp.AddComponent<MarkManager>();
            }
            if (!temp.GetComponent<Rigidbody2D>())
            {
                temp.AddComponent<Rigidbody2D>();
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            // snakeBody�� �߰�
            snakeBody.Add(temp);
            // �߰��� ������Ʈ�� bodyParts���� �����
            bodyParts.RemoveAt(0);
            // �߰��� ������Ʈ�� ����Ʈ�� ����
            temp.GetComponent<MarkManager>().ClearmMarkerList();
            // count �ʱ�ȭ
            countUp = 0;
        }

    }

}