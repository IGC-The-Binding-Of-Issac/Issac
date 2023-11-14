using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsKnife : ItemInfo
{
    public Sprite knifeImg;
    private void Start()
    {
        SetItemCode(13);
        SetItemString("������ ��Į",
                      "���� ������ �ʾҽ��ϴ�.",
                      "���� �� ������ ������� ��Į�� ������."
                    + "\n���ݷ� * 2"
                    + "\n���ݼӵ� / 2.5");
    }
    public override void UseItem()
    {
        //������ ������� ���� ��� Į�� �θ޶�ó�� ������.
        PlayerManager.instance.playerDamage *= 2f;
        PlayerManager.instance.playerShotDelay *= 2.5f;
        PlayerManager.instance.CheckedShotDelay();
        Invoke("GenerateKnife", 1.0f);
    }

    private void GenerateKnife()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().knifePosition.gameObject.SetActive(true);

    }
}
