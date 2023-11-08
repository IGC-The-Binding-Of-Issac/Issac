using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    public Sprite ipecacSpr;
    private void Start()
    {
       
        SetItemCode(9);
        SetItemString("������",
                      "���� �� ����..",
                      "���� �� ü�� �� ĭ ����" +
                      "\n������ ������ ���Ѵ�.");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerHp--;
        PlayerManager.instance.SetHeadSkin(1);
        PlayerManager.instance.SetBodySkin(1);
        if (!ItemManager.instance.PassiveItems[13] && !ItemManager.instance.PassiveItems[16])
        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = ipecacSpr;
    }
}