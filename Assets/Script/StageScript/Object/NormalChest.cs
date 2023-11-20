using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;
    [SerializeField] Sprite closeChestSprite;

    private void Start()
    {
        closeChestSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void ResetObject()
    {
        // �ʱ�ȭ
        gameObject.GetComponent<SpriteRenderer>().sprite = closeChestSprite;
        gameObject.layer = 15;

        // ������Ʈ ����.
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.gameObject.CompareTag("Player")) // Ǯ���ƿ� �浹��
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // �������� �̹����� ����
                OpenChest(); // ��� ������ ����
                openChestSound();
                StartCoroutine(StopChest()); // �и� ������Ʈ ���ߵ���
            }
    }

    IEnumerator StopChest()
    {
        gameObject.layer = 16;
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }

    void OpenChest()
    {

        ItemManager.instance.itemTable.Dropitem(transform.position, 1); // ���� �޾ƿ���
        // Before
        //for (int i = 0; i < 2; i++)
        //{
        //    int rd = Random.Range(0, 4);
        //    if (rd == 0)
        //    {
        //        int coin = Random.Range(0, 6);
        //        for (int j = 0; j < coin; j++)
        //        {
        //            GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
        //            GameManager.instance.roomGenerate.itemList.Add(it);
        //        }
        //    }
        //    else
        //    {
        //        GameObject it = Instantiate(ItemManager.instance.itemTable.OpenNormalChest(rd), transform.position, Quaternion.identity) as GameObject;
        //        GameManager.instance.roomGenerate.itemList.Add(it);
        //    }
        //}
    }

    void openChestSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
