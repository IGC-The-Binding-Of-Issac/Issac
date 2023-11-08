using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppyTail : ItemInfo
{
    private void Start()
    {
        SetItemCode(15);
        SetItemString("������ ����",
                      "�߸� ���� ����",
                      "���� �� �ִ� ü�� 1ĭ���� ����"
                    + "��� x9");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp = 2;
        PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        PlayerManager.instance.deathCount = 9;
    }
}
