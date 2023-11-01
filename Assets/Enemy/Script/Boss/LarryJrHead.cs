using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LarryJrHead : MonoBehaviour
{
    SnakeManager parent;

    void Start()
    {
        parent = transform.parent.GetComponent<SnakeManager>();
    }

    /// <summary>
    /// ����, player �浹
    /// 
    /// 1. �θ� rigidbody�� ����
    /// 2. �ڽ��� �浹�ص� ���� �浹ó���� �ذ� ����
    /// 
    /// </summary>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Larry �� + �Ӹ� , ���� �浹
        if (collision.gameObject.CompareTag("Tears"))
        {
            parent.getDamageLarry();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�÷��̾�� �浹 ���� �� (�ڽ�����)
        if (collision.gameObject.CompareTag("Player"))
        {
            parent.hitDamagePlayer();
        }
    }

}
