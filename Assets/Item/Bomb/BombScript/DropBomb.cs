using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private Animator gb;
    private Rigidbody2D rb;
    private int[] direction = { -1, 1 };
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gb = GetComponent<Animator>();

        int randomX = Random.Range(0, 2);
        int randomY = Random.Range(0, 2);
        //���� ���� �� (����, ������ / �� , �Ʒ�) ������ �������� ���� �����̱� ���� AddForce
        rb.AddForce(new Vector2(direction[randomX], direction[randomY]), ForceMode2D.Impulse);

        // ���� ��� �ִϸ��̼� �ʿ�.
        //gb.SetTrigger("DropBomb");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gb.SetTrigger("GetBomb");
            ItemManager.instance.bombCount++;
        }
    }


    //�Ծ��� �� �ִϸ��̼� �̺�Ʈ (������� �����)
    void GetBomb()
    {
        Destroy(gameObject);
    }
}
