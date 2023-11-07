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
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if (canUse)
        {
        PlayerManager.instance.playerMaxHp += 1;
        canUse = false;
        Invoke("SetCanUse", 1f);
        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
        Invoke("SetCanChangeItem", 1f);
        }
    }
}
