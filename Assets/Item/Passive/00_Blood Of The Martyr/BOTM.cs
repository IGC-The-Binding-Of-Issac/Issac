using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOTM : ItemInfo
{
    public GameObject tearPrf;
    public GameObject playerPrf;
    public Sprite redTearImg;
    public Sprite defaultTearImg;
    public Sprite MartyrImg;

    private void Start()
    {
        SetItemCode(0);
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerDamage++;
        //������ ĳ���� ���� ����� �޸�
        //playerPrf.transform.Find("HeadItem").GetComponent<SpriteRenderer>().sprite = MartyrImg; RŰ�� ������ �����.

        //���� ���� ���������� ����
        //tearPrf.gameObject.GetComponent<SpriteRenderer>().sprite = redTearImg; ���� ������ص� �̹����� �ʱ�ȭ �ȵ�

        //���� ������ �ִϸ��̼� ����
        
    }
}
