using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    public Sprite ipecacSpr;
    public override void Start()
    {
        base.Start();
        SetItemCode(9);
        SetItemString("������",
                      "���� �� ����..",
                      "���� �� ü�� �� ĭ ����" 
                    + "\n������ ������ ���Ѵ�.");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerHp--;
        UIManager.instance.DelHeart();
        PlayerManager.instance.playerTearSpeed *= 2f;
        PlayerManager.instance.CheckedPlayerHP();
        PlayerManager.instance.SetHeadSkin(1);
        PlayerManager.instance.SetBodySkin(1);
        PlayerManager.instance.SetTearSkin(2);
        //������ �������� �׸��� ���ư�
        //������ ������ �������� ������ �������� ��
    }
}