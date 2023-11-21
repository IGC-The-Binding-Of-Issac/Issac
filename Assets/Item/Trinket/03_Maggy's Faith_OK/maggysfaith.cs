using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maggysfaith : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropTearSpeed;
    float beforeDropRange;

    public override void Start()
    {
        base.Start();
        SetTrinketItemCode(3);
        SetTrinketString("�ű��� ����",
                         "������ �밡",
                         "���� �� ���� �ӵ� + 0.12" +
                         "\n��Ÿ� + 0.12");
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerTearSpeed += 0.12f;
        PlayerManager.instance.playerRange += 0.12f;
    }

    public override void DropTrinket()
    {
        beforeDropTearSpeed = PlayerManager.instance.playerTearSpeed;
        beforeDropRange = PlayerManager.instance.playerRange;

        PlayerManager.instance.playerTearSpeed = beforeDropTearSpeed - 0.12f;
        PlayerManager.instance.playerRange = beforeDropRange - 0.12f;
    }
}
