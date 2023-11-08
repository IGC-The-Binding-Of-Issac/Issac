using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HolyMantle : ItemInfo
{
    private void Start()
    {
        SetItemCode(6);
        SetItemString("신성한 망토",
                      "성스러운 방패",
                      "습득 시 캐릭터에게 최초 3회 방어막을 부여한다." 
                    + "\n이후 방 클리어 때마다 1회 방어막을 부여한다.");
    }
    public override void UseItem()
    {
        PlayerManager.instance.CanBlockDamage += 3;
    }
}
