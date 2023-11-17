using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dr_fetus : ItemInfo
{
    public GameObject attackBomb;
    public PlayerController ctr;
    private void Awake()
    {
        SetItemCode(16);
        SetItemString("�¾� �ڻ�",
                      "��ź �߻�",
                      "���� �� ���� �� ��ź�� �߻��Ѵ�."
                    + "���ݼӵ� * 0.5");
    }

    private void Start()
    {
        PlayerController ctr = GameManager.instance.playerObject.GetComponent<PlayerController>();
    }
    public override void UseItem()
    {
        if (ItemManager.instance.PassiveItems[13])
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().knifePosition.gameObject.SetActive(false);
            GameManager.instance.playerObject.GetComponent<PlayerController>().knife.SetActive(false);
            ItemManager.instance.PassiveItems[13] = false;
        }
        PlayerManager.instance.playerShotDelay /= 0.5f;
        PlayerManager.instance.tearObj = attackBomb;
    }
}
