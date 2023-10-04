using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroom : ItemInfo
{
    private void Start()
    {
        SetItemCode(4);
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp+=2;
        PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        PlayerManager.instance.playerMoveSpeed += 0.3f;
        PlayerManager.instance.playerDamage += 0.3f;
        PlayerManager.instance.playerDamage *= 1.5f;
        PlayerManager.instance.playerRange += 2.5f;
        //���� ũ�� 1.1�� ����
        //ĳ���� ũ�� 1.1�� ����
    }
}
