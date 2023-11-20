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

        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(ItemManager.instance.coinCount > 0 && canUse)
          {
            ItemManager.instance.coinCount--;
            PlayerManager.instance.playerDamage += 0.13f;
          }

        canUse = false;
        Invoke("SetCanUse", 1f);

        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
        Invoke("SetCanChangeItem", 1f);

    }
}
