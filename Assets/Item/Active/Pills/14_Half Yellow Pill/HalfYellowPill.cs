using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfYellowPill : ActiveInfo
{

    private void Awake()
    {
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
        Destroy(gameObject);
        }
    }
}
