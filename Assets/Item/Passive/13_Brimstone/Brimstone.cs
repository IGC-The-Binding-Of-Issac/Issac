using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : ItemInfo
{

    private void Start()
    {
        SetItemCode(13);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.33f;
        //������ �� ĳ���� �ܸ� ����

    }
}
