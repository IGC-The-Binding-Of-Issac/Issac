using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCat : ItemInfo
{
    private void Start()
    {
        SetItemCode(14);
        SetItemString("죽은 고양이",
                      "구피가 죽었어...",
                      "습득 시 최대 체력이 1칸으로 고정"
                    + "\n사망 시 50% 확률로 최대체력 회복 / 사망");
    }

    public override void UseItem()
    {
        //기능은 playerManager에 구현되어 있음. 위 SetItemString에 적힌 그대로
    }
}
