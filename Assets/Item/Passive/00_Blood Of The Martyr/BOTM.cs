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
        PlayerManager.instance.playerDamage++;
        //������ ĳ���� ���� ����� �޸�
        GameManager.instance.playerObject.transform.Find("HeadItem").GetComponent<SpriteRenderer>().sprite = MartyrImg;

        //���� ���� ���������� ����
        PlayerManager.instance.tearImage = redTearImg;
        //.gameObject.GetComponent<SpriteRenderer>().sprite = redTearImg; //���� ������ص� �̹����� �ʱ�ȭ �ȵ�

        //���� ������ �ִϸ��̼� ����
        
    }
}
