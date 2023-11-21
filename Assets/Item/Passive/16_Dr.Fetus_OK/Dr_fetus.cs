using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dr_fetus : ItemInfo
{
    public GameObject attackBomb;
    public PlayerController ctr;

    public override void Start()
    {
        base.Start();
        PlayerController ctr = GameManager.instance.playerObject.GetComponent<PlayerController>();
        SetItemCode(16);
        SetItemString("�¾� �ڻ�",
                      "��ź �߻�",
                      "���� �� ��ź�� �߻��Ѵ�."
                    + "\n���ݼӵ� * 0.5");
    }
    public override void UseItem()
    {
        if (ItemManager.instance.PassiveItems[13])
        {
            ctr.knifePosition.gameObject.SetActive(false);
            ctr.knife.SetActive(false);
            ItemManager.instance.PassiveItems[13] = false;
            PlayerManager.instance.playerDamage -= 2.0f;
        }
        PlayerManager.instance.playerShotDelay /= 0.5f;
    }
}
