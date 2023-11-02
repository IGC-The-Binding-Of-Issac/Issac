using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(5, 0);
        SetActiveString("???",
            "???",
            "???");

    }

    public override void UseActive()
    {
        if (canUse)
        {
        PlayerManager.instance.playerMoveSpeed += 0.15f;
        SetActiveString("�߿� �� �پ���!",
            "�̵� �ӵ� ����",
            "��� �� �̵� �ӵ��� �����Ѵ�.");
        Destroy(gameObject);
        }
    }
}

