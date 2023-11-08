using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPill : ActiveInfo
{
    
    private void Awake()
    {
        SetActiveItem(13, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerHp -= 1;
            SetActiveString("�������� �� �˾Ҵµ�..",
                            "ü�� ����",
                            "��� �� ���� ü���� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
