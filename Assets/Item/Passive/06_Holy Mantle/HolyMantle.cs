using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HolyMantle : ItemInfo
{
    private void Start()
    {
        SetItemCode(6);
    }

    private void SetHitDelay()
    {
        PlayerManager.instance.CanGetDamage = true;
    }
    public override void UseItem()
    {
        //�� ������ ���� �����. �ǰ� �� �� ������� 1�ʰ� ����.
        //�� �̵� �� �� �����
    }
}
