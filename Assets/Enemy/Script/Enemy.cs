using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    protected string dieParameter;
    protected Animator animator;

    [Header("Enemy State")]
    public bool playerInRoom;
    [SerializeField] protected float hp;
    protected float sight; //�þ� ����  
    protected float searchDelay; // ���� �ð� �ΰ� search
    protected float moveSpeed; 
    protected float bulletSpeed; // �Ѿ˼ӵ�
    protected float attaackSpeed; // ���ݼӵ�
    protected float waitforSecond; // �ױ��� wait

    [SerializeField] protected Transform playerPos; //���� �� �÷��̾� ��ġ
    // Move ���� ��ũ��Ʈ���� ����
    public virtual void Move() { }

    // �ʱ�ȭ
   
    //�÷��̾� search
    protected bool PlayerSearch()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 sightSize = new Vector2(x, y);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sightSize, sight); //���� ��ġ , ����

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                playerPos = colliders[i].transform;
                return true;
            }
        }
        return false;
    }

    //collider�˻�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //�÷��̾�� �ε����� �÷��̾��� hp����
            PlayerManager.instance.GetDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tears"))
        {
            Color oriColor = gameObject.GetComponent<SpriteRenderer>().color;
            StartCoroutine(Hit(oriColor));
        }
    }

    IEnumerator Hit(Color oriColor)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = oriColor;
    }

    // ������
    public void GetDamage(float damage) //Tears ���� ��� (player����� ��ŭ �� ����)
    {
        hp -= damage;
        if (IsDead())
        {
            DeadAction(dieParameter);
        }
    }

    // hp�˻�
    bool IsDead()
    {
        if (hp <= 0)
        {
            return true;
        }
        return false;
    }

    void DeadAction(string ani)
    {
        gameObject.GetComponent<Animator>().SetBool(ani, true);
        Destroy(gameObject , waitforSecond);
    }


}
