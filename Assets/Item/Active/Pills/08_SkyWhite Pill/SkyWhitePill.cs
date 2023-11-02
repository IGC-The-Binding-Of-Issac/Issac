using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWhitePill : ActiveInfo
{
    private void Awake()
    {
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
        Destroy(gameObject);
        }
    }
}
