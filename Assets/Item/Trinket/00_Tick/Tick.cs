using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tick : TrinketInfo
{
    float beforeDamage;
    float beforeMoveSpeed;
    float beforeShotDelay;
    private void Start()
    {
           SetTrinketItemCode(0);
            SetTrinketString("�����",
            "��, ¡�׷���",
            "���� �� ���ݷ� + 0.3" +
            "\n �̵��ӵ� - 0.24" +
            "\n ���� ü�� - 2ĭ" +
            "\n ���� �ӵ� - 0.2");
    }
    public override void GetItem()
    {
        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerMoveSpeed -= 0.24f;
        PlayerManager.instance.playerHp -= 2;
        PlayerManager.instance.playerShotDelay += 0.2f;
        if(PlayerManager.instance.playerHp <= 0)
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
        }
        PlayerManager.instance.CheckedShotDelay();
    }

    public override void DropTrinket()
    {
        beforeDamage = PlayerManager.instance.playerDamage;
        beforeMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        beforeShotDelay = PlayerManager.instance.playerShotDelay;
        PlayerManager.instance.playerDamage = beforeDamage - 0.3f;
        PlayerManager.instance.playerMoveSpeed = beforeMoveSpeed + 0.24f;
        PlayerManager.instance.playerShotDelay = beforeShotDelay - 0.2f;
    }
}
