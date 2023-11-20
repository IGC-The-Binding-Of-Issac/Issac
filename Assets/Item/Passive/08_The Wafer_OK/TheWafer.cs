using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWafer : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(8);
        SetItemString("�л�",
                      "ź��ȭ�� ���",
                      "���� �� �̵��ӵ� - 2"
                    + "\n���ݷ� + 2"
                    + "\n��Ÿ� - 2");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMoveSpeed -= 2f;
        PlayerManager.instance.playerDamage += 2f;
        PlayerManager.instance.playerRange -= 2f;
    }
}
