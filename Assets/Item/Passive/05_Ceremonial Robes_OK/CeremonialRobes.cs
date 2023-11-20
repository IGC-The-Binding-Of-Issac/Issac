using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeremonialRobes : ItemInfo
{

    private void Start()
    {
        SetItemCode(5);
        SetItemString("�ǽĿ� ����",
                      "��ο� �ΰ�",
                      "���� �� �ִ� ü�� + 2ĭ"
                    + "\n���ݷ� + 1");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp += 4;
        PlayerManager.instance.playerDamage += 1;

        //ĳ���� �ؽ�ó ����
        PlayerManager.instance.SetBodySkin(2);
        PlayerManager.instance.SetHeadSkin(4);
    }
}
