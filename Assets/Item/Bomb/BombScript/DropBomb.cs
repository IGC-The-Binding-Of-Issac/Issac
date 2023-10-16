using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    private Animator gb;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gb = GetComponent<Animator>();

        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        //���� ���� �� (����, ������ / �� , �Ʒ�) ������ �������� ���� �����̱� ���� AddForce
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);

        // ���� ��� �ִϸ��̼� �ʿ�.
        //gb.SetTrigger("DropBomb");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 31; // �÷��̾�� �浹�����ʴ� ���̾�
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
