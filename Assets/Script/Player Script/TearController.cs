using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearController : MonoBehaviour
{

    PlayerController playerController;
    Animator tearBoomAnim;

    Vector3 tearPosition;
    Vector3 playerPosition;

    float betweenDistance;

    // Start is called before the first frame update

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        tearBoomAnim = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� ��ġ
        playerPosition = playerController.transform.position;
        //�Ѿ� ��ġ
        tearPosition = this.transform.position;
        //�� ������ �Ÿ�
        betweenDistance = Vector3.Distance(tearPosition, playerPosition);

        //�� ������ �Ÿ��� �÷��̾� ��Ÿ����� Ŀ����
        if (betweenDistance >= GameController.instance.playerRange)
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

    public void DestoryTear()
    {
        //�Ѿ� �ı�
        Destroy(gameObject);
    }
}