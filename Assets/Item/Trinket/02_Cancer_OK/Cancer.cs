using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancer : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDropShotDelay;
    float beforeDropDamage;
    float beforeDropMoveSpeed;

    public override void Start()
    {
        base.Start();
        SetTrinketItemCode(2);
        SetTrinketString("�ϼ���",
                         "��, ���̴�!",
                         "���� �� ���ݼӵ� + 0.15" +
                         "\n���ݷ� - 0.2" +
                         "\n�̵� �ӵ� - 0.34");
    }

    public override void GetItem()
    {
        PlayerManager.instance.playerShotDelay -= 0.15f;
        PlayerManager.instance.CheckedShotDelay();
        PlayerManager.instance.playerDamage -= 0.2f;
        PlayerManager.instance.CheckedDamage();
        PlayerManager.instance.playerMoveSpeed -= 0.34f;
    }

    public override void DropTrinket()
    {
        beforeDropShotDelay = PlayerManager.instance.playerShotDelay;
        beforeDropDamage = PlayerManager.instance.playerDamage;
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;

        PlayerManager.instance.playerShotDelay = beforeDropShotDelay + 0.15f;
        PlayerManager.instance.CheckedShotDelay();
        PlayerManager.instance.playerDamage = beforeDropDamage +  0.2f;
        PlayerManager.instance.CheckedDamage();
        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed + 0.34f;
    }
}
