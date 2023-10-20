using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tear : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;
    Rigidbody2D tearRB;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;

    float playerTearSize;

    bool tmp;
    void Start()
    {
        tmp = true;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
        tearRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerTearSize = PlayerManager.instance.playerTearSize;
        TearRange();
    }

    void TearRange()
    {
        //�÷��̾� ��ġ
        playerPosition = playerController.transform.position;
        //�Ѿ� ��ġ
        tearPosition = this.transform.position;
        //�� ������ �Ÿ�
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);
        Debug.Log(PlayerManager.instance.playerRange);
        if(betweenDistance >= PlayerManager.instance.playerRange-0.4f && tmp)
        {
            tmp = false;
            tearRB.gravityScale = 10f;
        }
        //�� ������ �Ÿ��� �÷��̾� ��Ÿ����� Ŀ����
        if (betweenDistance >= PlayerManager.instance.playerRange)
        {
            //���� ������ �ִϸ��̼� ����
            tearBoomAnim.SetTrigger("BoomTear");
        }
    }

    public void StopTear()
    {
        tearRB.velocity = Vector2.zero;
        tearRB.gravityScale = 0.01f;
        //�Ѿ� ������Ʈ �ӵ��� zero�� ����

    }

    public void TearSize()
    {   
        gameObject.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
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