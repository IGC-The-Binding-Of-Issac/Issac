using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFingers : ActiveInfo
{
    
    void Awake()
    {
        SetActiveItem(1, 0);
        SetActiveString("���� �ȸ���",
            "1���� �־��ּ���",
            "��� �� 1���� �Ҹ��Ͽ� ���ݷ� + 0.13");
    }

    public override void UseActive()
    {
        if(ItemManager.instance.coinCount > 0)
        {
        ItemManager.instance.coinCount--;
        PlayerManager.instance.playerDamage += 0.13f;
        }
    }
}
