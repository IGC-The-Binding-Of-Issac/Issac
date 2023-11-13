using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Gurdy : Enemy
{
    [Header("BigAttackFly")]
    [SerializeField] float currTime; //���� ������ �ð�
    [SerializeField] bool chageState; // ���º�ȯ 

    [SerializeField] GameObject bigShootBullet;
    float stateTime;
    [SerializeField]int stateNum; // 1�� �Ѿ� �ջ�, 2�� �ĸ� ����
    int shootCount;
    Coroutine runningCoroutine = null;


    void Start()
    {

        playerInRoom = false;
        dieParameter = "";

        // Enemy
        hp = 100f;
        waitforSecond = 0.4f;
        attaackSpeed = 1.5f; // �Ѿ� �߻� �ϴ� �ð� 
        bulletSpeed = 3f;

        maxhp = hp;

        //Gurdy
        randTime(); 
        currTime = stateTime; //�ʱ� �ð� ����
        randNum();

        chageState = true;
        shootCount = 5; // �����ϴ� �Ѿ��� ����
    }

    private void Update()
    {
        if (playerInRoom)
            Move();
    }

    override public void Move()
    {


    }

    private void gurdyReset()
    {

    }

    void gurdyShoot() 
    {

    }
    
    void gurdyGeneFly() 
    {

    }

    void randTime()
    {
        //1f ~ 10f ���̿��� �ð�
        stateTime = Random.Range(2f, 5f);
    }

    void randNum()
    {
        // state 1.up , 2. down , 3.left , 4. right ���� �߿� �ϳ��� �������� ��
        stateNum = Random.Range(1, 3); // 1~2��
    }



}
