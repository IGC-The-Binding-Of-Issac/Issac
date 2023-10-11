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

        transform.position = Vector3.MoveTowards(transform.position, bulletDesti, bulletSpeed * Time.deltaTime); //�Ѿ� ������
        bulletDestroy(); // �����ϸ� destory
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� ���̸� ����
        {
            ani.SetBool("bulletDestroy" , true);
            PlayerManager.instance.GetDamage(); //�÷��̾�� ������
            Destroy(gameObject , waitForDest);
        }
        if (collision.gameObject.CompareTag("Wall")) // �� ���̸� ����
        {
            ani.SetBool("bulletDestroy", true);
            Destroy(gameObject, waitForDest);
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
