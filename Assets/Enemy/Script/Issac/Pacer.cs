using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer : TEnemy
{
    // prowl
    public override void En_setState()
    {
        playerInRoom    = false;
        dieParameter    = "isDie";

        hp              = 2f;
        sight           = 5f;
        moveSpeed       = 1.5f;
        waitforSecond   = 0.5f;
        fTime           = 0.5f;
        randRange       = 1f;

        maxhp           = hp;

        enemyNumber = 4;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = false;
        isProwl         = true;
        isDetective     = false;
        isShoot         = false;
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

    public override void e_ResetEnemy() { }
}
