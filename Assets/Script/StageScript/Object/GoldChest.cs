using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ItemManager.instance.keyCount > 0) // Ǯ���ƿ� �浹�� , ���谡 1�� �̻��϶�
        {
            ItemManager.instance.keyCount--; // ���� ���

            gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // �������� �̹����� ����
            OpenChest(); // ��� ������ ����
            StartCoroutine(StopChest());
        }
    }

    IEnumerator StopChest()
    {
        gameObject.layer = 16;
        yield return new WaitForSeconds(0.8f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void OpenChest()
    {
        GameObject it = Instantiate(ItemManager.instance.itemTable.OpenGoldChest(), transform.position, Quaternion.identity) as GameObject;
        GameManager.instance.roomGenerate.itemList.Add(it);
    }
}
