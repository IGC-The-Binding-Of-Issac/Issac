using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRedPill : ActiveInfo
{

    private void Awake()
    {
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
        SetActiveString("���� ��ġ��..",
        "���ݷ� ����",
        "��� �� ���ݷ��� �����Ѵ�.");
        Destroy(gameObject);
        }
    }
}
