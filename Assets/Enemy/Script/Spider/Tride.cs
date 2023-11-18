using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Playables;

public enum TrideState
{
    TrideIdle,
    TrideTracking, // ����
    TrideJump // ����
}


public class Tride : TEnemy
{
    public override void En_setState()
    {
        playerInRoom    = false;
        dieParameter    = "isDie";
        jumpParameter   = "isJump";

        hp              = 2f;
        sight           = 5f;
        moveSpeed       = 1.5f;
        waitforSecond   = 0.5f;
        attaackSpeed    = 3f;
        jumpSpeed       = 3f;

        maxhp           = hp;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = true;
        isProwl         = false;
        isDetective     = false;
        isShoot         = false;
        isJump          = true;
    }

    private void Start()
    {
        // ���� ���� state ����
        En_setState();              // ���� ����
        En_kindOfEnemy();           // enemy�� �ൿ ����

        En_Start();                  // �ʱ⼼��
    }

    private void Update()
    {
        E_Excute();                 // ���� ����
    }

}
