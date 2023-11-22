using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Maw : TEnemy
{
    // tracking, pworl, shoot, 
    public override void En_setState()
    {
        playerInRoom    = false;
        dieParameter    = "isDie";
        shootParameter  = "isShoot";

        hp              = 2f;
        sight           = 2f;
        moveSpeed       = 1.5f;
        attaackSpeed    = 1f;
        waitforSecond   = 0.5f;
        fTime           = 0.5f;
        randRange       = 1f;

        maxhp           = hp;

        enemyNumber = 3;
    }

    public override void En_kindOfEnemy()
    {
        isTracking      = true;
        isProwl         = true;
        isDetective     = true;
        isShoot         = true;
    }

    private void Start()
    {
        // ���� ���� state ����
        En_setState();               // ���� ����
        En_kindOfEnemy();            // enemy�� �ൿ ����

        En_Start();                  // �ʱ⼼��
    }

    private void Update()
    {
        E_Excute();                 // ���� ����
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sight);
    }

    public override void e_ResetEnemy() { }
}
