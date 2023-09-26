using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private void Start()
    {
        // ���� ��� �ִϸ��̼� �ʿ�.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetBomb();
            ItemManager.instance.bombCount++;
            Destroy(gameObject);
        }
    }


    void GetBomb()
    {
        // �Ծ����� ��ź �ִϸ��̼� �ۼ�.
    }
}
