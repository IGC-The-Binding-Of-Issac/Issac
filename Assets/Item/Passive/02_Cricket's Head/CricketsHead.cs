using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketsHead : ItemInfo
{
    private void Start()
    {
        SetItemCode(2);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage += 0.5f;
        PlayerManager.instance.playerDamage *= 1.5f;
        //������ ũ�Ⱑ(Scale) 1.1�� Ŀ����.
    }
}
