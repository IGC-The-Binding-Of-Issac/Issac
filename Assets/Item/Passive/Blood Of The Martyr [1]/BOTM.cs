using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTM : ItemInfo
{
    private void Start()
    {
        SetItemCode(1);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage++;
        //������ ĳ���� ���� ����� �޸�, ���� ���� ���������� ����
        //��ǿ� ����� �޸� ĳ���� ���� ����.
    }
}
