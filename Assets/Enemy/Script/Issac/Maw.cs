using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

enum MawState
{
    // Idle
    MawProwl,
    // �� ���
    MawShoot,
    //����
    MawTracking
}

public class Maw : TEnemy
{
    // �÷��̾� ����
    public override void En_setState()
    {
        playerInRoom = false;

        hp = 2f;
        sight = 5f;
        moveSpeed = 1.5f;
        waitforSecond = 0.5f;

        maxhp = hp;
    }

    public override void En_kindOfEnemy()
    {
        isTracking = true;
        isProwl = false;
        isDetective = false;
    }

    private void Start()
    {
        // ���� ���� state ����
        En_setState();              // ���� ����
        En_kindOfEnemy();           // enemy�� �ൿ ����
        En_stateArray();            // state �� �迭�� ����

        E_Enter();                  // ���� ���� (�⺻�� idle�� ���� �Ǿ�����)
    }

    private void Update()
    {
        E_Excute();                 // ���� ����
    }
}
