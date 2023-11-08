using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemInfo
{
    private void Start()
    {
        SetItemCode(2);
        SetItemString("�������� �Ź�",
                      "�� ���� ��",
                      "���� �� �̸��� ���� �� �� �� �����." +
                      "\n ������ 4������ ������.");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.42f;
        PlayerManager.instance.SetHeadSkin(2);
        PlayerManager.instance.CheckedShotDelay();
    }
}