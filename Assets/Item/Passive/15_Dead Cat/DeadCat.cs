using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCat : ItemInfo
{
    // Start is called before the first frame update
    private void Start()
    {
        SetItemCode(15);
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp = 2;
        if (PlayerManager.instance.playerMaxHp < PlayerManager.instance.playerHp)
            PlayerManager.instance.playerHp = PlayerManager.instance.playerMaxHp;
        //����� 3���� ���Ѵ�.
        //��� �� ��Ȱ �� ���������� ������Ѵ�.

    }
}
