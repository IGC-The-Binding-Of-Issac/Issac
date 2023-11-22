using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBlackPill : ActiveInfo
{
    public void Awake()
    {
        base.Start();
        SetActiveItem(16, 0);
        SetActiveString("???",
                        "???",
                        "???");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            PlayerManager.instance.playerHp -= 1;
            PlayerManager.instance.CheckedPlayerHP();
            UIManager.instance.SetPlayerCurrentHP();

            PlayerManager.instance.playerMoveSpeed += 0.08f;
            PlayerManager.instance.playerDamage += 0.15f;
            SetActiveString("������",
                            "ü�� ����, �̵��ӵ��� ���ݷ� ����",
                            "��� �� ���� ü���� ����������, �̵��ӵ��� ���ݷ��� �����Ѵ�.");
            UIManager.instance.ItemBanner(itemTitle, itemDescription);
            base.UseActive();
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            Destroy(gameObject);
        }
    }
}
