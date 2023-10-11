using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;

    bool chestState = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) // Ǯ���ƿ� �浹��
        {
            if(chestState) // �̹� ������������ üũ 
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // �������� �̹����� ����
                chestState = false; // �������·� ��ȯ
                OpenChest(); // ��� ������ ����
            }
        }
    }

    void OpenChest()
    {
        for (int i = 0; i < 2; i++)
        {
            int rd = Random.Range(0, 4);
            if (rd == 0)
            {
                int coin = Random.Range(0, 6);
                for (int j = 0; j < coin; j++)
                {
                    GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
                }
            }
            else
            {
                GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
            }
        }
    }
}
