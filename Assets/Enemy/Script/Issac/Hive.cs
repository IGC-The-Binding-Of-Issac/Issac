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

        hp = 10f;
        sight = 3f;
        moveSpeed = 5f;
        waitforSecond = 0.5f;


        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
    }

    void Update()
    {
        if (playerInRoom)
            Move();
    }

    public override void Move()
    {

        //�÷��̾ ���� �ȿ� ������
        if (PlayerSearch()) 
        {

            x = gameObject.transform.position.x;
            y = gameObject.transform.position.y;
            hiveMoveToX = (x - playerPos.transform.position.x) * 2; // hive ��ġ - �÷��̾� x
            hiveMoveToY = (y - playerPos.transform.position.y) * 2; // hive ��ġ - �÷��̾� y

            Vector3 vector3 = new Vector3(hiveMoveToX, hiveMoveToY, 0);
            transform.position = Vector3.MoveTowards(transform.position, vector3, moveSpeed * Time.deltaTime);
        }

    }

    // ��������
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
