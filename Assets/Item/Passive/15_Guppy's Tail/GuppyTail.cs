using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppyTail : ItemInfo
{
    private void Start()
    {
        SetItemCode(15);
        SetItemString("������ ����",
                      "�߸� ���� ����",
                      "");
    }
    public override void UseItem()
    {
     //���� �� ĳ���Ͱ� ����� ������ 50% Ȯ���� �������� �����
    }
}
