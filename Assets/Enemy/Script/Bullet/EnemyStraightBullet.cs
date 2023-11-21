using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStraightBullet : MonoBehaviour
{
    Animator ani;
    float bulletSpeed;
    float waitForDest;

    void Start()
    {
        ani = GetComponent<Animator>();
        waitForDest = 0.5f;
        bulletSpeed = 5f;
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� ���̸� ����
        {
            ani.SetBool("bulletDestroy", true);
            PlayerManager.instance.GetDamage(); //�÷��̾�� ������
            Destroy(gameObject, waitForDest);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock")) // �� ���̸� ����
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            Destroy(gameObject, waitForDest);
        }

        //�˿� ������
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
            collision.gameObject.GetComponent<Poop>().GetDamage();
        }
        //�ҿ� ������
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
            collision.gameObject.GetComponent<FirePlace>().GetDamage();
        }
    }

}
