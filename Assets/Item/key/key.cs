using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator keyAni;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);
        keyAni = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 31;
            keyAni.SetTrigger("GetKey");
        }
    }

    public void GetKey()
    {
        ItemManager.instance.keyCount++;
        Destroy(this.gameObject);
    }
}
