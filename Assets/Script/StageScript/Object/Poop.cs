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
        if(poopIndex >= 3) // ü���� ���� ���̸�
        {
            DestorySound();
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //�ݶ��̴� ���ֱ�.
            ItemDrop();
        }
    }

    void ChangeSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = poopSprite[poopIndex];
    }

    void ItemDrop()
    {
        int rd = Random.Range(0, 3);
        if (rd <= 0)
        {
            GameObject it = Instantiate(ItemManager.instance.itemTable.ObjectBreak(), transform.position, Quaternion.identity) as GameObject;
            GameManager.instance.roomGenerate.itemList.Add(it);
            
        }
    }

    void DestorySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
