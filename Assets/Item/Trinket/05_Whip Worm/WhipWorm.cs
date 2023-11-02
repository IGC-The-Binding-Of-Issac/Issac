using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWorm : TrinketInfo
{
    float beforeRange;
    float beforeShotDelay;
    private void Start()
    {
        SetTrinketItemCode(5);
        SetTrinketString("ä�� ����",
            "Ȫġ!",
            "���� �� ��Ÿ� + 0.2" +
            "\n ���� �ӵ� - 0.3");
    }

    public override void GetItem()
    {
        beforeRange = PlayerManager.instance.playerRange;
        beforeShotDelay = PlayerManager.instance.playerShotDelay;
        PlayerManager.instance.playerRange += 2f;
        PlayerManager.instance.playerShotDelay += 0.3f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerRange = beforeRange;
        PlayerManager.instance.playerShotDelay = beforeShotDelay;
    }
}
