using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ItemInfo
{
    TheNail()
    {
        itemCode = 26;
        itemName = "The Nail";
        item_Damage = 2.0f;
        item_MoveSpeed = -0.18f;
        item_MaxHP = 1.0f;
    }
    //��� ��
    //ĳ���Ͱ� �Ǹ��� ���Ѵ�. + �� �� �ְ� �ȴ�. (����)
    //�ִ� ü�� + 0.5ĭ
    //ĳ������ ������ ������ ���Ѵ�.
    //���� �μ� �� �ְ� �ȴ�. (����)
}
