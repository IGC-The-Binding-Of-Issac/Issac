using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : Top_Fly
{
    // ���� �ȿ� ������ ���� + �c�� �߽�
    // ���� �ȿ� ������ ���� ������
   
    enum FooterState 
    {
        // Idle
        PooterProwl,
        // �� ���
        PooteShoot,
        //����
        PooteTracking
    }
    [SerializeField] FooterState state;
    [SerializeField] GameObject enemyBullet; //�Ѿ� ������
    float currTime;
    float oriMoveSpeed;


    // ���� ���� ���� ������ + �÷��̾ �ȿ� ������ ����
    void Start()
    {
        Fly_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 1f;
        sight = 4f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;
        attaackSpeed = 3f; // idle <-> Shoot

        //TopFly
        randRange = 1f;
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
                state = FooterState.PooterProwl;
                return;
            }

            //�÷��̾ ���� �ȿ� ���� ��, �ð� ���� �� ��� , �̵��ϰ�
            else if (PlayerSearch()) 
            {
                currTime -= Time.deltaTime;
                if (currTime > 0)
                {
                    state = FooterState.PooteTracking;
                    Lookplayer();
                    moveSpeed = oriMoveSpeed;
                    return;
                }

                else if (currTime <= 0)
                {
                    state = FooterState.PooteShoot;
                    //moveSpeed = 0;
                    animator.SetTrigger("isShoot");
                    currTime = attaackSpeed;
                }
            }
        }


    }

    override public void Move()
    {
        switch (state) 
        {
            case FooterState.PooterProwl:
                Prwol();
                break;
            case FooterState.PooteShoot:
                PooterShoot();
                break;
            case FooterState.PooteTracking:
                Tracking(playerPos);
                break;
        }
        
    }

    // ����
    void PooterShoot() 
    {
        GameObject bulletobj = Instantiate(enemyBullet , transform.position , Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }
}
