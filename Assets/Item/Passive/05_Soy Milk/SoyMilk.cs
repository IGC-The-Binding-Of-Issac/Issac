using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoyMilk : ItemInfo
{
    private void Start()
    {
        SetItemCode(5);
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerDamage *= 0.2f;
        PlayerManager.instance.playerShotDelay /= 5.5f;

        //���� ũ�� 0.3��� ����
        //������ �ʿ��� ������ ���� ��� ����, ������ ���� �ߵ� (������ ��)
    }
}
