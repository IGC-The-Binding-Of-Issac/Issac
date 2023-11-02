using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(12, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerDamage += 0.08f;
        PlayerManager.instance.playerMoveSpeed += 0.08f;
        SetActiveString("��������!",
        "���ݷ� ����, �̵��ӵ� ����",
        "��� �� ���ݷ°� �̵��ӵ��� �����Ѵ�.");
        Destroy(gameObject);
    }
}
