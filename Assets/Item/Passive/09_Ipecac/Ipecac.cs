using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    private void Start()
    {
        itemCode = 10;
        //SetItemCode(9);
    }

    public override void UseItem()
    {
            PlayerManager.instance.playerHp--;
        //������ �� �Ӽ��� ���Ѵ�. 
        //�� ������ �ʴ� [playerDamage] ��ŭ�� ���ظ� ������.
    }
}