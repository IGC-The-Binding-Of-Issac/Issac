using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : ItemInfo
{
    private void Start()
    {
        SetItemCode(7);
        SetItemString("������",
            "���ݷ� ����",
            "���� �� ���ݷ� + 1" +
            "\n�ִ� ü�� + 1ĭ");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage += 1.0f;
        PlayerManager.instance.playerMaxHp += 2;
        //�Ǹ���/õ��� ����Ȯ�� + 10%
    }
}
