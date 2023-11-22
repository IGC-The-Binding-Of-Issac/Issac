using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    /// <summary>
    /// 
    ///  - �Ѿ� pooling -
    /// pooling�� �ϳ� ����� ���� , ���� ���Ͱ� pooling�� ����
    /// 
    /// </summary>

    public static EnemyPooling Instance;

    [SerializeField]
    private GameObject straightBullet;
    private GameObject followBullet;

    Queue<EnemyBullet> poolingStraightBullet = new Queue<EnemyBullet>();
    Queue<EnemyBullet> poolingFollowBullet = new Queue<EnemyBullet>();

    private void Awake()
    {
        Instance = this;

        // ���߿� playermanager...? ���� �ʱ�ȭ�ϱ�
        EnemyBullet_Initialize(10);
    }

    // �ʱ�ȭ ,  queue�� �̸� initCount ��ŭ �־��
    private void EnemyBullet_Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++) 
        {
            poolingStraightBullet.Enqueue(createStraightBullet());  //queue �� �� �߰�
            poolingFollowBullet.Enqueue(createFollowBullet());
        }
    }

    // straight bullet ����
    private EnemyBullet createStraightBullet() 
    {
        EnemyBullet newObj = Instantiate(straightBullet).GetComponent<EnemyBullet>();
        newObj.gameObject.SetActive(false);             // queue�� ���� ���� �� ���̰� 
        newObj.transform.SetParent(transform);          // ���� �� ��ũ��Ʈ�� ����ִ� �� ������Ʈ�� �θ��
        return newObj;
    }

    // follow Bullet ����
    private EnemyBullet createFollowBullet()
    {
        EnemyBullet newObj = Instantiate(followBullet).GetComponent<EnemyBullet>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);          // ���� �� ��ũ��Ʈ�� ����ִ� �� ������Ʈ�� �θ��
        return newObj;
    }

    // �ٸ� ��ũ��Ʈ���� straightBullet ������ �� ����ϴ�
    public static EnemyBullet GetStraightBullet() 
    {
        EnemyBullet obj;

        // straight pool�� ������Ʈ�� ����� ������
        if (Instance.poolingStraightBullet.Count > 0)
        {
            obj = Instance.poolingStraightBullet.Dequeue(); // queue���� �� �տ� �ִ°� ����
        }
        // straight pool�� ������Ʈ�� ����� ���� ������
        else
        {
            obj = Instance.createStraightBullet();      // ���ο� �Ѿ� �����
            // return �� �� queue�� �ǵ����� , ���⼭ queue �� �ȳ־��൵��
        }

        // ������ ������Ʈ�� return
        obj.gameObject.SetActive(true);                         // pooling �迭���� setActive(false)�� �س��� �� ������ ���̰� ����
        obj.transform.SetParent(null);                          // �θ� ������
        return obj;
    }

    // �ٸ� ��ũ��Ʈ���� straightBullet ������ �� ����ϴ�
    public static EnemyBullet GetFollowBullet()
    {
        EnemyBullet obj;

        // straight pool�� ������Ʈ�� ����� ������
        if (Instance.poolingStraightBullet.Count > 0)
        {
            obj = Instance.poolingFollowBullet.Dequeue(); // queue���� �� �տ� �ִ°� ����
        }
        // straight pool�� ������Ʈ�� ����� ���� ������
        else
        {
            obj = Instance.createFollowBullet();      // ���ο� �Ѿ� �����
            // ������ return �� �� queue�� �ǵ����� , ���⼭ queue �� �ȳ־��൵��
        }

        // ������ ������Ʈ�� return
        obj.gameObject.SetActive(true);                         // pooling �迭���� setActive(false)�� �س��� �� ������ ���̰� ����
        obj.transform.SetParent(null);                          // �θ� ������
        return obj;
    }

    // �ٸ� ������Ʈ���� �Ѿ��� �����ϰ� �ı��ɶ�, 
    // return���� pooling�迭�� �־���
    // straight �Ѿ� return
    public static void returnStrightBullet(EnemyBullet obj) 
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingStraightBullet.Enqueue(obj); 
    }

    // Follow �Ѿ� return
    public static void returnFollowBullet(EnemyBullet obj) 
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent (Instance.transform);
        Instance.poolingFollowBullet.Enqueue(obj);
    }


    

}
