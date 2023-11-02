using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebraPill : ActiveInfo
{

    private void Awake()
    {
        SetActiveItem(15, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerMoveSpeed -= 0.15f;
        PlayerManager.instance.playerShotDelay -= 0.08f;
        SetActiveString("�������� �����ٰ�.",
        "�̵��ӵ� ����, ���ݼӵ� ����",
        "��� �� �̵��ӵ��� ����������, ���ݼӵ��� �����Ѵ�.");
        Destroy(gameObject);
    }
}
