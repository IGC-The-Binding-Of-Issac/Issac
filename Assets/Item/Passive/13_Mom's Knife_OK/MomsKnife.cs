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
                    + "\n���ݷ� + 2");
    }
    public override void UseItem()
    {
        if (!ItemManager.instance.PassiveItems[16])
        {
        //������ ������� ���� ��� Į�� �θ޶�ó�� ������.
        PlayerManager.instance.playerDamage += 2.0f;
        Invoke("GenerateKnife", 1.0f);
        }
    }

    private void GenerateKnife()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().knifePosition.gameObject.SetActive(true);
        GameManager.instance.playerObject.GetComponent<PlayerController>().knife.SetActive(true);
    }
}
