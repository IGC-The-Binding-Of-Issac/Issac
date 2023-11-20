using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    int poopIndex = -1;

    [Header("Unity SetUp")]
    [SerializeField] Sprite[] poopSprite;
    Sprite defaultSprite;
    private void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void GetDamage()
    {
        poopIndex++;
        ChangeSprite(poopIndex);
        if(poopIndex >= 3) // ü���� ���� ���̸�
        {
            DestorySound();
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //�ݶ��̴� ���ֱ�.
            ItemDrop();
        }
    }

    void ChangeSprite(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = poopSprite[index];
    }

    void ItemDrop()
    {
        int rd = Random.Range(0, 3);
        if (rd <= 0)
        {
            ItemManager.instance.itemTable.Dropitem(transform.position, rd);

        }
    }

    void DestorySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void ResetObject()
    {
        // �ʱ�ȭ
        poopIndex = -1;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        GetComponent<BoxCollider2D>().enabled = true;

        // ������Ʈ ����.
        gameObject.SetActive(false);
    }
}
