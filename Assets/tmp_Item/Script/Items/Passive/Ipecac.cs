using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : ItemInfo
{
    Ipecac()
    {
        itemCode = 10;
        itemName = "Ipecac";
        item_Hp = -0.5f; //30�ʸ���
        item_AttackSpeed = -0.1f;
    }
    //������ �� �Ӽ��� ���Ѵ�
    //�� ������ �ʴ� [PlayerDamage]��ŭ�� ���ظ� ������.
}
