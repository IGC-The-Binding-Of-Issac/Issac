using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindrange : TrinketInfo
{
    float beforeDropRange;
    float beforeDropDamage;
    float beforeDropMoveSpeed;
    float beforeDropShotDelay;
    private void Start()
    {
        SetTrinketItemCode(6);
        SetTrinketString("�� �� �г�",
            "������ ������ ���ÿ�",
            "���� �� ��Ÿ� - 0.99"
            + "\n ���ݷ� + 3"
            + "\n �̵��ӵ� - 1.2");
    }

    public override void GetItem()
    {
        if (PlayerManager.instance.playerRange > 3) PlayerManager.instance.playerRange *= 0.25f;
        PlayerManager.instance.playerDamage += 5f;
        PlayerManager.instance.playerShotDelay -= 0.25f;
        PlayerManager.instance.playerMoveSpeed -= 1.2f;
        PlayerManager.instance.CheckedShotDelay();
    }

    public override void DropTrinket()
    {
        beforeDropRange = PlayerManager.instance.playerRange;
        beforeDropDamage = PlayerManager.instance.playerDamage;
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;
        PlayerManager.instance.playerRange = beforeDropRange /= 0.25f;
        PlayerManager.instance.playerDamage = beforeDropDamage -= 5f;
        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed += 1.2f;
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay += 0.25f;
    }
}
