using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOTM : ItemInfo
{
    public Sprite redTearImg;
    public Sprite MartyrImg;


    private void Start()
    {
        SetItemCode(0);

    }
    public override void UseItem()
    {
        //�÷��̾� ������ + 1
        PlayerManager.instance.playerDamage++;

        //���� ���� ���������� ����
        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = redTearImg;

        //����� �޸��� �Լ� 1�ʵ� ����
        Invoke("getBOTM", 1f);

        //���� ������ �ִϸ��̼� ����

    }

    public void getBOTM()
    {
        //������ ĳ���� ���� ����� �޸�
        //GameManager.instance.playerObject.transform.Find("HeadItem").GetComponent<SpriteRenderer>().sprite = MartyrImg;
        GameManager.instance.playerObject.GetComponent<PlayerController>().HeadItem.GetComponent<SpriteRenderer>().sprite = MartyrImg;
    }
}
