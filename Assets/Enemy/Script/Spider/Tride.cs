using System;
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
    [SerializeField] Vector3 jumpDesti; //���� �� ��ǥ ��ġ
    [SerializeField] Vector3 tridePosi; //���� �� ��ǥ ��ġ
    float currTime;
    float oriSpeed;
    float jumpSpeed;

    [SerializeField] float jumpAiPlayTime;

    bool trackingStarted;
    bool jumpStarted;

    void Start()
    {
        trideState = TrideState.TrideIdle;

        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";
        animator = GetComponent<Animator>();

        //Enemy
        hp = 3f;
        sight = 3f;
        moveSpeed = 2f;
        waitforSecond = 0.4f;
        attaackSpeed = 2f;

        //Tride
        currTime = 0;
        oriSpeed = moveSpeed;
        jumpSpeed = 4f;

        trackingStarted = false; //���� �ڷ�ƾ ����
        jumpStarted = false; //���� �ڷ�ƾ ����
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
            if (!trackingStarted)
            {
                // state��ȯ�� �� �ѹ� ����
                ChageState(TrideState.TrideTracking);

                trackingStarted = true;
                jumpStarted = false;

                GetJumpTime();
                // tracnking ���°� ������ ������ / ���� �� �ð� (jumpAiPlayTime)����
            }
        }
        else if (currTime < attaackSpeed + (jumpAiPlayTime * 10)) // jumpAiPlayTime ��ŭ ����
        {
            if (!jumpStarted)
            {
                // state��ȯ�� �� �ѹ� ����
                ChageState(TrideState.TrideJump);
                jumpStarted = true;
            }

        }
        else
        {

            // Ÿ�̸Ӹ� �缳���ϰ� "Tracking" ���·� ���ư�
            //Debug.Log("���¸� �ٲ��~");
            trackingStarted = false;
            jumpStarted = true;

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

    IEnumerator TrideTracking()
    {
        // z���� 0 ����
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        while (true)
        {
            moveSpeed = oriSpeed;
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }

    IEnumerator TrideJump()
    {
        // ���� jumpAiPlayTime��ŭ jump �ִϸ��̼� ����
        animator.SetFloat("isJump", jumpAiPlayTime);
        // z������ 1�ø���
        transform.position = new Vector3(transform.position.x, transform.position.y, 1 );

        while (true)
        {
            moveSpeed = jumpSpeed;
            Tracking(justTrackingPlayerPosi);
            yield return null;

        }
    }

    //jump�ִϸ��̼��� ���� �� �ð� ���ϱ�
    void GetJumpTime()
    {
        // ���� ��ġ �޾ƿ�
        jumpDesti = new Vector3(justTrackingPlayerPosi.position.x, justTrackingPlayerPosi.position.y, 0);
        // tride��ġ 1ȸ �޾ƿ�
        tridePosi = new Vector3(transform.position.x, transform.position.y, 0);

        //�Ÿ�, �ӷ�(jumpSpeed), �ð� ���� ���, �ð� ���ϱ�
        float plyerTotride = Vector3.Distance(jumpDesti, tridePosi);
        jumpAiPlayTime = plyerTotride / jumpSpeed; // (����)�ð� = �÷��̾�� tride�Ÿ� / ���� �ӵ�
    }

}
