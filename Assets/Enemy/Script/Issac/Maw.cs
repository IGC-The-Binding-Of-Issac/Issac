using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Maw : Top_IssacMonster
{
    // ���� �ȿ� ������ ���� + �c�� �߽�
    // ���� �ȿ� ������ ���� ������
    // Pooter�� �ڵ� ����

    enum MawState
    {
        // Idle
        PooterProwl,
        // �� ���
        PooteShoot,
        //����
        PooteTracking
    }
    [SerializeField] MawState state;
    [SerializeField] GameObject enemyBullet; //�Ѿ� ������
    float currTime;
    float oriMoveSpeed;


    // ���� ���� ���� ������ + �÷��̾ �ȿ� ������ ����
    void Start()
    {

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 5f;
        sight = 4f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;
        attaackSpeed = 3f; // idle <-> Shoot

        //TopFly
        randRange = 0.5f;
        fTime = 0.5f;
        StartCoroutine(checkPosi(randRange));

        //Footer
        currTime = attaackSpeed;
        oriMoveSpeed = moveSpeed;
       
    }

    private void Update()
    {
        if (playerInRoom)
        {
            Move();
            //�÷��̾ ���� �ȿ� ���� ��
            if (!PlayerSearch())
            {
                state = MawState.PooterProwl;
                return;
            }

            //�÷��̾ ���� �ȿ� ���� ��, �ð� ���� �� ��� , �̵��ϰ�
            else if (PlayerSearch())
            {
                currTime -= Time.deltaTime;
                if (currTime > 0)
                {
                    state = MawState.PooteTracking;
                    Lookplayer();
                    moveSpeed = oriMoveSpeed;
                    return;
                }

                else if (currTime <= 0)
                {
                    state = MawState.PooteShoot;
                    //moveSpeed = 0;
                    //animator.SetTrigger("isShoot");
                    currTime = attaackSpeed;
                }
            }
        }


    }

    override public void Move()
    {
        switch (state)
        {
            case MawState.PooterProwl:
                Prwol();
                break;
            case MawState.PooteShoot:
                PooterShoot();
                break;
            case MawState.PooteTracking:
                Tracking(playerPos);
                break;
        }

    }

    // ����
    void PooterShoot()
    {
        GameObject bulletobj = Instantiate(enemyBullet, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
