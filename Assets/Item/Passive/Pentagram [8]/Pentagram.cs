using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : ItemInfo
{
    private void Start()
    {
        SetItemCode(8);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage += 1.0f;
        PlayerManager.instance.playerMaxHp += 2;
        //�Ǹ���/õ��� ����Ȯ�� + 10%
    }
}
