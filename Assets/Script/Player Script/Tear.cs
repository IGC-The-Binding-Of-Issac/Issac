using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tear : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;
    Enemy enemy;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;

    float playerTearSize;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        playerTearSize = PlayerManager.instance.playerTearSize;
        TearRange();
        TearSize();
    }

    void TearRange()
    {
        //�÷��̾� ��ġ
        playerPosition = playerController.transform.position;
        //�Ѿ� ��ġ
        tearPosition = this.transform.position;
        //�� ������ �Ÿ�
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);

        //�� ������ �Ÿ��� �÷��̾� ��Ÿ����� Ŀ����
        if (betweenDistance >= PlayerManager.instance.playerRange)
        {
            //���� ������ �ִϸ��̼� ����
            tearBoomAnim.SetTrigger("BoomTear");
        }
    }

    public void StopTear()
    {
        //�Ѿ� ������Ʈ �ӵ��� zero�� ����
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void TearSize()
    {   
        gameObject.transform.localScale = new Vector3(playerTearSize, playerTearSize, playerTearSize);
    }

    public void DestoryTear()
    {
        //�Ѿ� �ı�
        Destroy(gameObject);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        //���� ������ �Ѿ� ��Ʈ����
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");

        }
        //�˿� ������
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<Poop>().GetDamage();
        }
        //�ҿ� ������
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<FirePlace>().GetDamage();
        }
        //���� ������
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            tearBoomAnim.SetTrigger("BoomTear");
            collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);

            //Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();

            //�Ѿ� ����
            Vector2 direction = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().knockBack());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction*200);
        }
    }
}