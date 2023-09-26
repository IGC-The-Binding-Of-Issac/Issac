using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    int poopIndex = -1;

    [Header("Unity SetUp")]
    [SerializeField] Sprite[] poopSprite;
    public void GetDamage()
    {
        poopIndex++;
        ChangeSprite();
        if(poopIndex == 3) // ü���� ���� ���̸�
        {
            ItemDrop();
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //�ݶ��̴� ���ֱ�.
        }
    }

    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = poopSprite[poopIndex];
    }

    void ItemDrop()
    {
        // ItemManage���� ������ ���� ����غ��ô�.
        //����,ü�� �� ��������� Ȯ�����. ���� �ʿ�.
    }
}
