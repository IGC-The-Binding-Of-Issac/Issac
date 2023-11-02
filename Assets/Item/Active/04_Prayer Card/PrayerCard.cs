using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerCard : ActiveInfo
{
    void Awake()
    {
        SetActiveItem(4, 4);
        SetActiveString("�⵵�� ī��",
            "���밡���� ����",
            "��� �� �ִ� ü�� + 1ĭ");
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerMaxHp += 1;
    }

    public override void CheckedItem()
    {
        
    }
}
