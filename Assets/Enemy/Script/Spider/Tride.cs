using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tride : Top_Spider
{
    // �濡 ������ ����,
    // ���� �ð� ���� ���� (���� �ۿ� ������ �ƾ� �۷� �ָ� ����)
    enum TrideState 
    {
        Tracking, //����
        Jump // ����
            
    }


    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //�ֻ��� enemy
        hp = 20f;
        sight = 3f;
        moveSpeed = 10f;
        waitforSecond = 0.5f;
    }

    private void Update()
    {
        if (playerInRoom) 
        { }
    }

    public override void Move()
    {
       
    }
}
