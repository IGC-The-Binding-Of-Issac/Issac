using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPill : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(12, 0);
        SetActiveString("???",
        "???",
        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerDamage += 0.08f;
        PlayerManager.instance.playerMoveSpeed += 0.08f;
        SetActiveString("��������!",
        "���ݷ� ����, �̵��ӵ� ����",
        "��� �� ���ݷ°� �̵��ӵ��� �����Ѵ�.");
        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
        Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
