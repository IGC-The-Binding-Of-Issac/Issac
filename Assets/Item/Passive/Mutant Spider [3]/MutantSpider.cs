using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantSpider : ItemInfo
{
    private void Start()
    {
        SetItemCode(3);
    }

    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.42f;
        //�̸��� ���� �� �� �� �����.
        //ĳ������ ��� ������ ������ �߻��Ѵ�. [4��]
        
    }
}
