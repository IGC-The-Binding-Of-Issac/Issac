using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class BigAttackFly : Top_Fly
{
    /// <summary>
    /// 
    /// 1. �ſ� õõ�� �÷��̾� ����
    /// 2. ���� �ð����� �ѹ��� ���鼭 �������� ������ �Ѿ� �ջ�
    /// 
    /// </summary>

    [Header("BigAttackFly")]
    [SerializeField] float currTime; //���� ������ �ð�
    [SerializeField] bool chageState; // ���º�ȯ 
    [SerializeField]  float rotSpeed;
    [SerializeField] GameObject bigShootBullet;

    float z = 0;
    bool coruState;
    Coroutine runningCoroutine = null;

    void Start()
    {         
        Fly_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isBigFlyDie";

        // Enemy
        hp = 30f;
        sight = 5f;
        moveSpeed = 1f;
        waitforSecond = 0.4f;
        attaackSpeed = 4f;
        bulletSpeed = 6f;

        maxhp = hp;

        //BigAttackFly
        chageState = true;
        currTime = attaackSpeed;
        rotSpeed = 600f;
        chageState = true;
        coruState = true;
    }

    private void Update()
    {
        justTrackingPlayerPosi = GameObject.FindWithTag("Player").transform;
        if (justTrackingPlayerPosi == null)
            return;

        if (playerInRoom)
            Move();
    }

    override public void Move()
    {
        currTime -= Time.deltaTime;
        if (currTime > 0 && chageState)
        {
            Tracking(justTrackingPlayerPosi); //����
        }
        else if (currTime <= 0) 
        {
            chageState = false;
            bigAttackFlyRotation();
        }
    }

    public void bigAttackFlyRotation()
    {
        if (coruState)
        {
            // StopCoroutine�� �ȵ��ư��� ->
            // �ڷ�ƾ ���� runnungCorutine�� ���� ����� ���ÿ� ���� 
            runningCoroutine = StartCoroutine(ShootBullets());
            coruState = false;
        }

        //ȸ��
        // ȸ������ ������ �ſ� -> ����Ƽ����  ���� ȸ������ ������ ��������
        // ���Ϸ� ��� : ������ ���󶧹� -> ������ ���ʹϾ� ������� (����ϸ� �Ҽ���)
        // ���Ϸ� ������� ����ϰ�;�� -> transform.rotation.eulerAngles

        z += rotSpeed * Time.deltaTime; //���� �ð� (Time.deltaTime) ���� z���� ���Ѵ�
        transform.rotation = Quaternion.Euler(0, 0, z);
        //�Ѿ˹߽�



        if(transform.rotation.eulerAngles.z >= 350)  //������ 360�� �Ǹ� �ʱ�ȭ
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);// ȸ�� �ʱ�ȭ
            if (runningCoroutine != null) //�������� �ڷ�ƾ�� ������
            {
                // ���߱�
                StopCoroutine(runningCoroutine);
            }

            // �ʱ�ȭ
            z = 0; // ���� �ʱ�ȭ
            coruState = true;
            chageState = true;
            currTime = attaackSpeed;
        }
    }

    IEnumerator ShootBullets()
    {
        while (true) 
        {
            bool isbullet = true;
            if (isbullet) 
            {
                GameObject bu = Instantiate(bigShootBullet, transform.position, transform.rotation);
                bu.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
                isbullet = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }


}
