using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maggysfaith : TrinketInfo
{
    float beforeTearSpeed;
    float beforeRange;

    private void Start()
    {
        SetTrinketItemCode(3);
        SetTrinketString("�ű��� ����",
            "������ �밡",
            "���� �� ���� �ӵ� + 0.12" +
            "\n ��Ÿ� + 0.12");
    }
    public override void GetItem()
    {
        beforeTearSpeed = PlayerManager.instance.playerTearSpeed;
        beforeRange = PlayerManager.instance.playerRange;
        PlayerManager.instance.playerTearSpeed += 0.12f;
        PlayerManager.instance.playerRange += 0.12f;
    }

    public override void DropTrinket()
    {
        PlayerManager.instance.playerTearSpeed = beforeTearSpeed;
        PlayerManager.instance.playerRange = beforeRange;
    }
}
