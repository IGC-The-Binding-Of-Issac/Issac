using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeremonialRobes : ItemInfo
{


    private void Start()
    {
        SetItemCode(5);
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 4;
        PlayerManager.instance.playerDamage += 1;

        //ĳ���� �ؽ�ó ����
    }
}
