using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

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

    [SerializeField] TrideState trideState;
    float currTime;
    float trideJumpTime;
    float oriSpeed;
    float jumpSpeed;

    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 5f;
        sight = 4f;
        moveSpeed = 2f;
        waitforSecond = 0.8f;
        attaackSpeed = 3f; // stay <-> move

        //Spider
        currTime = 0;
        trideJumpTime = 1.2f + attaackSpeed;
        oriSpeed = moveSpeed;
        jumpSpeed = moveSpeed * 2;
    }
    private void Update()
    {
        if (playerInRoom)
        {
            justTrackingPlayerPosi = GameObject.FindGameObjectWithTag("Player").transform;
            Move();
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
        else if (currTime < trideJumpTime)
        {
            ChageState(TrideState.TrideJump);
        }
        else
        {
            // Ÿ�̸Ӹ� �缳���ϰ� "Tracking" ���·� ���ư�
            animator.SetBool("isJump", false);
            moveSpeed = oriSpeed;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            currTime = 0;
            ChageState(TrideState.TrideTracking);
        }
    }

    void ChageState(TrideState newState)
    {
        // ���� ���� ����
        StopCoroutine(trideState.ToString());
        // ���ο� ���·� ����
        trideState = newState;
        // ���� ������ �ڷ�ƾ ����
        StartCoroutine(trideState.ToString());
    }

    //���� 
    IEnumerator TrideTracking()
    {
        while (true)
        {
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }
    IEnumerator TrideJump()
    {
        moveSpeed = jumpSpeed;
        animator.SetBool("isJump", true);
        transform.position += new Vector3(0, 0, -1) * Time.deltaTime;
        while (true)
        {
            Tracking(justTrackingPlayerPosi);
            yield return null;
        }
    }
}
