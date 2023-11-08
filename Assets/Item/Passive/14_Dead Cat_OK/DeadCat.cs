using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCat : ItemInfo
{
    // Start is called before the first frame update
    private void Start()
    {
        SetItemCode(14);
        SetItemString("���� �����",
                      "���ǰ� �׾���...",
                      "���� �� �ִ� ü���� 1ĭ���� ����"
                    + "\n��� �� 50% Ȯ���� �ִ�ü�� ȸ�� / ���");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp = 2;
        if (PlayerManager.instance.playerMaxHp < PlayerManager.instance.playerHp)
        {
            PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        }

        if (PlayerManager.instance.playerHp == 0)
        {
            PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        }
    }
}
