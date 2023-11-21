using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRedPill : ActiveInfo
{

    public override void Start()
    {
        base.Start();
        SetActiveItem(10, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerDamage -= 0.15f;
            PlayerManager.instance.CheckedDamage();
            SetActiveString("���� ��ġ��..",
                            "���ݷ� ����",
                            "��� �� ���ݷ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
