using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(7, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage += 0.15f;
        SetActiveString("������ ��!",
        "���ݷ� ����",
        "��� �� ���ݷ��� �����Ѵ�.");
        Destroy(gameObject);
    }
}
