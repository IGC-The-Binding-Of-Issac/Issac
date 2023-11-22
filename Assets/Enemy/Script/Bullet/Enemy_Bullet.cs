using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    protected Animator ani;
    protected float bulletSpeed;
    protected float waitForDest;
    protected bool isCoru;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� ���̸� ����
        {
            ani.SetBool("bulletDestroy", true);
            PlayerManager.instance.GetDamage(); //�÷��̾�� ������

        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock")) // �� ���̸� ����
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
        }

        //�˿� ������
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
            collision.gameObject.GetComponent<Poop>().GetDamage();
        }
        //�ҿ� ������
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
            collision.gameObject.GetComponent<FirePlace>().GetDamage();
        }

        //���� ������
        else if (collision.gameObject.CompareTag("Object_Rock"))
        {
            ani.SetBool("bulletDestroy", true);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

            //StartCoroutine(EnemyPooling.Instance.waitReturn(waitForDest, this.gameObject)); // pooling return
        }
    }

}
