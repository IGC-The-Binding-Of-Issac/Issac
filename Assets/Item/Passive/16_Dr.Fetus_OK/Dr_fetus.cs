using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dr_fetus : ItemInfo
{
    public GameObject attackBomb;
    private void Awake()
    {
        SetItemCode(16);
        SetItemString("�¾� �ڻ�",
            "��ź �߻�",
            "");
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.33f;
        PlayerManager.instance.tearObj = attackBomb;
    }
}
