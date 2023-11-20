using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatHoof : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropMoveSpeed;
    float beforeDropShotDelay;
    void Awake()
    {
        SetTrinketItemCode(1);
        SetTrinketString("���� �߱�",
                         "�̵� �ӵ� ����, ���� �ӵ� ����",
                         "���� �� �̵��ӵ� + 0.16" +
                         "\n���ݼӵ� - 0.16");
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerMoveSpeed += 0.16f;
        PlayerManager.instance.playerShotDelay += 0.16f;
    }
    public override void DropTrinket()
    {
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;

        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed - 0.16f;
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay - 0.16f;
    }
}

