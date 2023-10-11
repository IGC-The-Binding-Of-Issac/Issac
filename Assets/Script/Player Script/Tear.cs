using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tear : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;

    float playerTearSize;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
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

    public void KnockBack()
    {
        Vector2 a = gameObject.transform.GetComponent<Rigidbody2D>().velocity;
        a.y = a.y* -1f;
        a.x = a.x * -1f;
        Debug.Log(a.x);
        Debug.Log(a.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //���� ������ �Ѿ� ��Ʈ����
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Object_Rock"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
        }
        //�˿� ������
        else if (collision.gameObject.CompareTag("Object_Poop"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<Poop>().GetDamage();
        }
        //�ҿ� ������
        else if (collision.gameObject.CompareTag("Object_Fire"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
            collision.GetComponent<FirePlace>().GetDamage();
        }
        //���� ������
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            tearBoomAnim.SetTrigger("BoomTear");
            collision.gameObject.GetComponent<Enemy>().GetDamage(PlayerManager.instance.playerDamage);
            if (collision.gameObject.GetComponent<Enemy>())
            {

            }
            KnockBack();
        }
    }
}