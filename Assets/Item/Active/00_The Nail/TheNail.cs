using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ActiveInfo
{
    public Sprite redTearImg;

    private void Awake()
    {
        //0�� ������, 6ĭ �ʿ���
        SetActiveItem(0, 6);
        SetActiveString("���", 
            "�Ͻ��� �Ǹ� ����",
            "��� �� ĳ���Ͱ� �Ǹ��� ���Ͽ� ������ ȿ���� ��´�."
            + "\n �ִ� ü�� + 0.5ĭ"
            + "\n �̵� �ӵ� - 0.08" 
            + "\n ���ݷ� + 0.5"
            + "\n ĳ������ ������ ������ ���Ѵ�.");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerMaxHp += 1;
        PlayerManager.instance.playerMoveSpeed -= 0.08f;
        PlayerManager.instance.playerDamage += 0.5f;

        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = redTearImg;
        Invoke("SetCanUse", 1f);
        }
    }
}
