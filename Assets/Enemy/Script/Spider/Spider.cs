using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Top_Spider
{
    //  ���� ������ + �÷��̾� �����ϸ� �÷��̾����� ������׷� ���󰡱�(?)

    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //�ֻ��� enemy
        hp = 2f;
        sight = 3f;
        moveSpeed = 5f;
        waitforSecond = 0.5f;

        //���� Top_Spider
        randRange = 10f;
        fTime = 0.5f; 
        StartCoroutine(checkPosi(randRange));
        
    }

    void Update()
    {
        if (playerInRoom)
        {
            Move();
        }
    }

    public override void Move()
    {
        Prwol();
    }

}
