using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCat : ItemInfo
{
    // Start is called before the first frame update
    private void Start()
    {
        SetItemCode(14);
        SetItemString("���� �����",
                      "���ǰ� �׾���...",
                      "���� �� �ִ� ü���� 1ĭ���� ����"
                    + "\n��� �� 50% Ȯ���� �ִ�ü�� ȸ�� / ���");
    }

    public override void UseItem()
    {
        //����� playerManager�� �����Ǿ� ����.
    }
}
