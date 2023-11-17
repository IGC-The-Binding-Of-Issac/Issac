using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footer : TEnemy
{
    // �÷��̾� ����
    public override void En_setState()
    {
        playerInRoom         = false;

        hp                   = 2f;
        sight                = 2f;
        moveSpeed            = 1.5f;
        waitforSecond        = 0.5f;
        fTime                = 0.5f;
        randRange            = 1f;

        maxhp                = hp;
    }

    public override void En_kindOfEnemy()
    {
        isTracking          = true;
        isProwl             = true;
        isDetective         = true;
        isShoot             = true;
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
}
