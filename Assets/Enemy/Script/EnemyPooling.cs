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

    [Header("pooling")]
    [SerializeField] Transform[] childArr;
    // index 0 : ����
    // index 1 : straightBullet �� ���� ����
    // index 2 : followBullet ``
    [SerializeField] Transform straightPooling_parent;  // �Ѿ��� ��� ���� �θ� (�� ������Ʈ)
    [SerializeField] Transform followPooling_Parent;    // ``

    [Header("pooling ������Ʈ")]
    [SerializeField] private GameObject straightBullet;
    [SerializeField] private GameObject followBullet;

    [Header("pooliong queue")]
    Queue<GameObject> poolingStraightBullet     = new Queue<GameObject>();
    Queue<GameObject> poolingFollowBullet       = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        // ���߿� playermanager...? ���� �ʱ�ȭ�ϱ�
        EnemyBullet_Initialize(10);
    }

    // �ʱ�ȭ ,  queue�� �̸� initCount ��ŭ �־��
    private void EnemyBullet_Initialize(int initCount)
    {
        childArr = gameObject.GetComponentsInChildren<Transform>();         // �ڽĿ�����Ʈ�� �迭��
        straightPooling_parent  = childArr[1];                       // �迭�� 1
        followPooling_Parent    = childArr[2];                       // �迭�� 2 

        for (int i = 0; i < initCount; i++) 
        {
            poolingStraightBullet.Enqueue(createStraightBullet());  //queue �� �� �߰�
            poolingFollowBullet.Enqueue(createFollowBullet());
        }
    }

    // straight bullet ����
    private GameObject createStraightBullet() 
    {
        GameObject newObj = Instantiate(straightBullet) as GameObject;
        newObj.gameObject.SetActive(false);                         // queue�� ���� ���� �� ���̰� 
        newObj.transform.SetParent(straightPooling_parent);         // �θ���
        return newObj;
    }

    // follow Bullet ����
    private GameObject createFollowBullet()
    {
        GameObject newObj = Instantiate(followBullet) as GameObject;
        newObj.gameObject.SetActive(false);                        // queue�� ���� ���� �� ���̰� 
        newObj.transform.SetParent(followPooling_Parent);          // �θ���
        return newObj;
    }

    // �ٸ� ��ũ��Ʈ���� straightBullet ������ �� ����ϴ�
    public static GameObject GetStraightBullet() 
    {
        GameObject obj;

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
    public static GameObject GetFollowBullet()
    {
        GameObject obj;

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
    public void returnBullet(GameObject obj) 
    {
        // straight ���� follow ���� �˻��ؼ� 
        // ���� �ش��ϴ� queue�� �־������

        if (obj is EnemyStraightBullet)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(straightPooling_parent);
            Instance.poolingStraightBullet.Enqueue(obj);            // �ǵ��ƿ��� �ٽ� pooling �迭�� �־���
        }

        else if (obj is EnemyFollowBullet) 
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(followPooling_Parent);
            Instance.poolingFollowBullet.Enqueue(obj);            // �ǵ��ƿ��� �ٽ� pooling �迭�� �־���
        }
      
    }

    

  
}
