using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrimStone : ItemInfo
{
    BrimStone()
    {
        itemCode = 12;
        itemName = "BrimStone";
        item_DamageMulti = 0.33f;
    }
    // ������ : ƽ�� [playerDamage]�� ���ظ� ������
    // ������ 1�� �� 9ƽ�� ���ӽð��� ����.
    // ����(2��)�� �Ϸ�Ǿ�� �߻�, 
    // �� ���� �߻��� ��� ������ ��ҵȴ�.
    // ���� �ð��� ���� �ӵ��� �ݺ���Ѵ�.
}
