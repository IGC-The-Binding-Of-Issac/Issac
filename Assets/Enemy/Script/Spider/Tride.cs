using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum TrideState 
{
    TrideIdle,
    TrideTracking, // ����
    TrideJump // ����
}


public class Tride : Top_Spider
{
    // �濡 ������ ����,
    // ���� �ð� ���� ���� (�÷��̾ ���� �ٷ�)

    // ���ѻ��� �ӽ� ��� (�ڷ�ƾ)
    // yield return null : �ش� ���·� �Ѿ���� , �ٸ� ���·� �Ѿ �� ���� while�� ����

    [Header("Tride")]
    [SerializeField] TrideState trideState;
    float currTime;
    float jumpCoolTIme;
    float oriSpeed;
    float jumpSpeed;
    float trideX;
    float trideY;
    float trideZ;

    void Start()
    {
        ChageState(TrideState.TrideIdle); //�ʱ� ���´� ������ �ִ�, 
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";
        animator = GetComponent<Animator>();

        //Enemy
        hp = 1f;
        sight = 3f;
        moveSpeed = 2f;
        waitforSecond = 0.8f;
        attaackSpeed = 2f;

        //Tride
        currTime = 0;
        jumpCoolTIme = 1f;
        oriSpeed = moveSpeed;
        jumpSpeed = 7f;
    }

    private void Update()
    {
        justTrackingPlayerPosi = GameObject.FindWithTag("Player").transform;
        if (justTrackingPlayerPosi == null)
            return;

        if (playerInRoom) 
        {
            Move();
            Lookplayer(justTrackingPlayerPosi);
        }
    }

    public override void Move()
    {
        //���� ��ȭ
        currTime += Time.deltaTime;
        if (currTime < attaackSpeed)
        {
            ChageState(TrideState.TrideTracking);
        }
        else if (currTime <attaackSpeed + jumpCoolTIme)
        {

            ChageState(TrideState.TrideJump);
        }
        else
        {
            // Ÿ�̸Ӹ� �缳���ϰ� "Tracking" ���·� ���ư�
            animator.SetBool("isJump", false);
            currTime = 0;
            ChageState(TrideState.TrideTracking);
        }


    }


    // �÷��̾��� �ൿ�� newState�� ��ȯ
    void ChageState(TrideState newState) 
    {
        // ���� ���� ����
        StopCoroutine(trideState.ToString());
        // ���ο� ���·� ����
        trideState = newState;
        // ���� ������ �ڷ�ƾ ����
        StartCoroutine(trideState.ToString());
    }

    
    IEnumerator Idle() 
    {
        //���� �ʱ�ȭ
        yield return null;
    }

    IEnumerator Tracking()
    {
        while (true) 
        {
            moveSpeed = oriSpeed;
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }
    IEnumerator Jump()
    {
        animator.SetBool("isJump", true);
        while (true) 
        {
            moveSpeed = jumpSpeed;
            Tracking(justTrackingPlayerPosi);
            //Tride�� �����ϴ� �ڵ�
            yield return null;

        }
    }

    void TrideJump() 
    {
        // ��ǥ ��ġ �޾ƿ� (�÷��̾� ��ġ)

        trideX = justTrackingPlayerPosi.position.x;
        trideY = justTrackingPlayerPosi.position.y;
        trideZ = justTrackingPlayerPosi.position.z;

        // x�� , y�� �÷��̾� �������� tracking�ϵ���
        // z�� : �׳� �ٷ� 1�� ������ٰ� ������ 0���� �ٲٱ�


    }
}
