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
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerHp += 1;
        PlayerManager.instance.playerMaxHp += 1;
        SetActiveString("�ܴ�������",
        "ü�� ����",
        "��� �� �ִ� ü�� �� ü���� �����Ѵ�.");
        Destroy(gameObject);
    }
}
