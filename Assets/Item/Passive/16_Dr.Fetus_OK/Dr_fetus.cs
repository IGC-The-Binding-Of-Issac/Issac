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
        PlayerManager.instance.playerShotDelay /= 0.5f;
        PlayerManager.instance.tearObj = attackBomb;
    }
}
