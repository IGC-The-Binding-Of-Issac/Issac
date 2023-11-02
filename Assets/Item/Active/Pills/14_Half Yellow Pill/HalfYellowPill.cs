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
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerRange += 0.15f;
        SetActiveString("�� �⸰�̾�.",
        "��Ÿ� ����",
        "��� �� ������ ��Ÿ��� �����Ѵ�.");
        Destroy(gameObject);
    }
}
