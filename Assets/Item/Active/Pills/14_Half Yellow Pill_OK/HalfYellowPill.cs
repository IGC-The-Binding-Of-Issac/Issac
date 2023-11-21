using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfYellowPill : ActiveInfo
{
    public override void Start()
    {
        base.Start();
        SetActiveItem(14, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerRange += 0.15f;
            SetActiveString("�� �⸰�̾�.",
                            "��Ÿ� ����",
                            "��� �� ������ ��Ÿ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
