using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HolyMantle : ItemInfo
{
    private void Start()
    {
        SetItemCode(6);
        SetItemString("�ż��� ����",
                      "�������� ����",
                      "3ȸ ���� �ο��Ѵ�." 
                    + "\n�� Ŭ���� ������ 1ȸ ���� �ο��Ѵ�.");
    }
    public override void UseItem()
    {
        PlayerManager.instance.CanBlockDamage += 3;
    }
}
