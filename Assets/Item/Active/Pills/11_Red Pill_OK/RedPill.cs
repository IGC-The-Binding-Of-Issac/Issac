using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(11, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerHp += 2;
            PlayerManager.instance.playerMaxHp += 2;
            SetActiveString("�ܴ�������",
                            "ü�� ����",
                            "��� �� �ִ� ü�� �� ü���� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
