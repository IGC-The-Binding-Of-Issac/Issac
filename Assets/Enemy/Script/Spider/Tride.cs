using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrideState 
{
    Idle,
    Tracking, // ����
    Jump // ����
}


public class Tride : Top_Spider
{
    // �濡 ������ ����,
    // ���� �ð� ���� ���� (�÷��̾ ���� �ٷ�)
    [SerializeField] TrideState trideState;

    void Start()
    {
        ChageState(TrideState.Idle); //�ʱ� ���´� ������ �ִ�, 
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        hp = 20f;
        sight = 3f;
        moveSpeed = 10f;
        waitforSecond = 0.5f;
    }

    private void Update()
    {
        if (playerInRoom) 
        {
            Move();
        }
    }

    public override void Move()
    {
       // state��ȭ ��Ű�� ��ũ��Ʈ �ۼ�

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
        //���·� ���� �� �� 1ȸ ���
        Debug.Log("Tride : " + trideState);

        while (true) 
        {
            Debug.Log("Tride�� �÷��̾ ���� �ؾ���");
            yield return null;
        }
    }
    IEnumerator Jump()
    {
        //���·� ���� �� �� 1ȸ ���
        Debug.Log("Tride : " + trideState);

        while (true) 
        {
            Debug.Log("Tride�� ������ �ؾ��� ");
            yield return null;
        }
    }
}
