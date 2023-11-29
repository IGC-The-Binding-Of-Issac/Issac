using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : TrinketInfo
{
    [Header("beforeStatement")]
    float beforeDamage;
    float beforeMoveSpeed;
    float beforeShotDelay;
    float beforeRange;
    float beforeTearSpeed;

    public override void Start()
    {
        base.Start();
        SetTrinketItemCode(0);
        SetTrinketString("�����",
                         "��, ¡�׷���",
                         "���� �� ���ݷ� + 0.3" 
                       + "\n�̵��ӵ� - 0.24" 
                       + "\n���� ü�� - 2ĭ" 
                       + "\n���� �ӵ� + 0.2"
                       + "\n��Ÿ� - 0.2"
                       + "\n���� �ӵ� - 2");
    }
    public override void GetItem()
    {
        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerMoveSpeed -= 0.24f;
        PlayerManager.instance.playerHp -= 2;
        UIManager.instance.SetPlayerCurrentHP();
        PlayerManager.instance.playerTearSpeed -= 2;
        PlayerManager.instance.playerShotDelay += 0.2f;
        PlayerManager.instance.playerRange -= 0.2f;
        PlayerManager.instance.CheckedStatus();
    }

    public override void DropTrinket()
    {
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeShotDelay = PlayerManager.instance.playerShotDelay;
        beforeRange = PlayerManager.instance.playerRange;
        beforeTearSpeed = PlayerManager.instance.playerTearSpeed;

        PlayerManager.instance.playerDamage = beforeDamage - 0.3f;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed + 0.24f;
        PlayerManager.instance.playerShotDelay = beforeShotDelay - 0.2f;
        PlayerManager.instance.playerRange = beforeRange + 0.2f;
        PlayerManager.instance.playerTearSpeed = beforeTearSpeed + 2;
        PlayerManager.instance.CheckedStatus();
    }
}
