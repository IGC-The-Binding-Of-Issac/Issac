using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Animator ani;
    [SerializeField] Vector3 bulletDesti;
    [SerializeField] Transform playerPosi;

    float bulletSpeed;
    float waitForDest;


    bool isMoving = true;
    private void Start()
    {
        ani = GetComponent<Animator>();
        playerPosi = GameObject.FindWithTag("Player").transform; //�÷��̾��� ��ġ
        bulletDesti = new Vector3(playerPosi.position.x, playerPosi.position.y, 0);

        waitForDest = 0.5f;
        bulletSpeed = 5f;
    }

    private void Update()
    {
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, bulletDesti, bulletSpeed * Time.deltaTime); //�Ѿ� ������
            bulletDestroy(); // �����ϸ� destory
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� ���̸� ����
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            PlayerManager.instance.GetDamage(); //�÷��̾�� ������
            Destroy(gameObject, waitForDest);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock")) // �� ���̸� ����
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
        }

        //�˿� ������
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
            collision.gameObject.GetComponent<Poop>().GetDamage();
        }
        //�ҿ� ������
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            isMoving = false;
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
            collision.gameObject.GetComponent<FirePlace>().GetDamage();
        }
    }

    // �ʱ⿡ ������ �Ÿ��� �����ϸ� Destory
    void bulletDestroy() 
    {
        if (Vector3.Distance(gameObject.transform.position , bulletDesti) <= 0.5f) 
        {
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
        }
    }
}
