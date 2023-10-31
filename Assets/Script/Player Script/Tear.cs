using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;
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

    private void Awake()
    {
    }
    void Start()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
        tearRB = GetComponent<Rigidbody2D>();
        //�÷��̾� �߻� ������ġ
        playerPosition = playerController.transform.position;
    }

    void Update()
    {
        TearRange();
    }

    void TearRange()
    {
        //�Ѿ� ��ġ
        tearPosition = this.transform.position;
        //�� ������ �Ÿ�
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);
        //�� ������ �Ÿ��� �÷��̾� ��Ÿ����� Ŀ����
        if (betweenDistance >= PlayerManager.instance.playerRange)
        {
            BoomTear();
        }
    }

    public void StopTear()
    {
        tearRB.velocity = Vector2.zero;
        //�Ѿ� ������Ʈ �ӵ��� zero�� ����
    }

    public void BoomTear()
    {
        if (ItemManager.instance.PassiveItems[0] == true || (ItemManager.instance.ActiveItem != null && ItemManager.instance.ActiveItem.name == "The Nail(Clone)" && ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().activated))
        {
            tearBoomAnim.SetTrigger("RedBoomTear");
        }
        else
        {
            //���� ������ �ִϸ��̼� ����
            tearBoomAnim.SetTrigger("BoomTear");
        }
    }

    public void TearSize()
    {
        playerTearSize = PlayerManager.instance.playerTearSize;
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
            BoomTear();
        }

        //�˿� ������
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            BoomTear();
            collision.GetComponent<Poop>().GetDamage();
        }
        //�ҿ� ������
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            BoomTear();
            collision.GetComponent<FirePlace>().GetDamage();
        }
        //���� ������
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            BoomTear();
            collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);

            //Rigidbody2D enemyRB = collision.gameObject.GetComponent<Rigidbody2D>();

            //�Ѿ� ����
            Vector2 direction = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
            //�˹�
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().knockBack());
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction*200);
        }
    }
}