using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHalo : ItemInfo
{
    private void Start()
    {
        SetItemCode(12);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 2;
        PlayerManager.instance.playerShotDelay -= 0.07f;
        PlayerManager.instance.playerDamage -= 0.3f;
        //ĳ���Ͱ� �ϴ��� �� �� �ְ� �ȴ�.

    }
}
