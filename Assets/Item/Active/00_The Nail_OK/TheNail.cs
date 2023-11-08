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
                        + "\n�ִ� ü�� + 1ĭ"
                        + "\n�̵� �ӵ� - 0.08" 
                        + "\n���ݷ� + 0.5"
                        + "\nĳ������ ������ ������ ���Ѵ�.");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerMaxHp += 2;
            PlayerManager.instance.playerMoveSpeed -= 0.08f;
            PlayerManager.instance.playerDamage += 0.5f;

            PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = redTearImg;
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Invoke("SetCanUse", 1f);
        }
    }
}
