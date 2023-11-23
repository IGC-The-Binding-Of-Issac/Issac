using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyMilk : ItemInfo
{
    public override void Start()
    {
        base.Start();
        SetItemCode(4);
        SetItemString("����",
                      "�� ������..",
                      "���� �� ���ݷ� * 0.2" 
                    + "\n���ݼӵ� * 3.5");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerDamage *= 0.2f;
        PlayerManager.instance.playerShotDelay /= 3.5f;
        PlayerManager.instance.playerTearSize *= 0.45f;
        PlayerManager.instance.ChgTearSize();
        base.UseItem();
    }
}
