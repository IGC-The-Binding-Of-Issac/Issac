using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPill : ActiveInfo
{
    
    private void Awake()
    {
        SetActiveItem(13, 0);
        SetActiveString("???",
        "???",
        "???");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerHp -= 1;
        SetActiveString("�������� �� �˾Ҵµ�..",
        "ü�� ����",
        "��� �� ���� ü���� �����Ѵ�.");
        Destroy(gameObject);
    }
}
