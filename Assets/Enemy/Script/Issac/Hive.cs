using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : Enemy
{
    [SerializeField] protected float x;
    [SerializeField] protected float y;

    [SerializeField] float hiveMoveToX;
    [SerializeField] float hiveMoveToY;

    void Start()
    {
        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        hp = 10f;
        sight = 3f;
        moveSpeed = 5f;
        waitforSecond = 0.5f;

    }

    void Update()
    {
        if (playerInRoom)
            Move();
    }

    public override void Move()
    {

        //�÷��̾ ���� �ȿ� ������
        

    }

    // ��������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
