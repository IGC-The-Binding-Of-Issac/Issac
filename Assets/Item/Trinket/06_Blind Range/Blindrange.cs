using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blindrange : TrinketInfo
{
    float beforeRange;
    float beforeDamage;
    float beforeMoveSpeed;
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
        beforeRange = PlayerManager.instance.playerRange;
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        if (PlayerManager.instance.playerRange > 3) PlayerManager.instance.playerRange *= 0.25f;
        PlayerManager.instance.playerDamage += 5f;
        PlayerManager.instance.playerShotDelay -= 0.25f;
        PlayerManager.instance.playerMoveSpeed -= 1.2f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerRange = beforeRange;
        PlayerManager.instance.playerDamage = beforeDamage;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed;
    }
}
