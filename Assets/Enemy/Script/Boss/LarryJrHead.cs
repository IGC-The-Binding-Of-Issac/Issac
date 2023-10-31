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

    // �����浹
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tears"))
        {
            parent.getDamageLarry();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            parent.hitDamagePlayer();
        }
    }

}
