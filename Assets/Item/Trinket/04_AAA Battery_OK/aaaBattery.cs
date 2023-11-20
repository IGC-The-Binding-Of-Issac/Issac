using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaBattery : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropMoveSpeed;
    float beforeDropShotDelay;

    private void Start()
    {
        SetTrinketItemCode(4);
        SetTrinketString("AAA ������",
                         "������",
                         "���ݼӵ� + 0.05" 
                       + "\n�̵��ӵ� + 0.1");
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerShotDelay -= 0.05f;
        PlayerManager.instance.playerMoveSpeed += 0.1f;
    }

    public override void DropTrinket()
    {
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;

        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed -= 0.1f;
        PlayerManager.instance.playerShotDelay = beforeDropShotDelay += 0.05f;
    }
}
