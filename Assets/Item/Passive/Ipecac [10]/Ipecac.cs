using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    private float time = 0f;
    private void Start()
    {
        SetItemCode(10);
    }

    public override void UseItem()
    {
            PlayerManager.instance.playerHp--;
        //������ �� �Ӽ��� ���Ѵ�. 
        //�� ������ �ʴ� [playerDamage] ��ŭ�� ���ظ� ������.
    }
}
