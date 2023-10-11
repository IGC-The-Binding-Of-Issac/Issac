using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Top_Spider
{
    //  ���� ������ + �÷��̾� �����ϸ� �÷��̾����� ������׷� ���󰡱�(?)
    enum SpiderState
    {
        iDle , //������
        Move , // ������
        Tracking // ����

    }
    [SerializeField] SpiderState state;
    float currTime;

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

        //���� Top_Spider
        randRange = 2f;
        fTime = 0.5f; 
        StartCoroutine(checkPosi(randRange));
        
        currTime = fTime;
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



    // ��������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
