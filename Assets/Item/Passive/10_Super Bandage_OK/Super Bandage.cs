using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBandage : ItemInfo
{

    private void Start()
    {
        SetItemCode(10);
        SetItemString("���� ��â��",
            "�׸� ����! �ʹ� ����!",
            "���� �� �ִ� ü�� - 1ĭ"
            + "\n ü�� - 1ĭ"
            + "\n ���ݼӵ� + 0.1");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerMaxHp-=2;
        PlayerManager.instance.playerHp-=2;
        PlayerManager.instance.playerShotDelay-=0.1f;
        //ĳ������ ������ ������ ���Ѵ�.
        
    }
}
