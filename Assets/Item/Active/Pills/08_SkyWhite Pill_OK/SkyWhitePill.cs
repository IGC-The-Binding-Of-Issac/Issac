using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWhitePill : ActiveInfo
{
    public override void Start()
    {
        base.Start();
        SetActiveItem(8, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerRange -= 0.15f;
            SetActiveString("���� ���߰� �ʴ�",
                            "��Ÿ� ����",
                            "��� �� ������ ��Ÿ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
