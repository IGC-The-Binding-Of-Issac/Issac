using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum SpiderState 
{ 
    SpiderStay,
    SpiderRandMove
}

public class Spider : Top_Spider
{
    //  ���� ������
    [SerializeField] SpiderState state;
    float currTime;
    float spdierMoveTime;

    void Start()
    {
        Spider_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        animator = GetComponent<Animator>();
        hp = 5f;
        sight = 4f;
        moveSpeed = 2f;
        waitforSecond = 1f;
        attaackSpeed = 0.5f; // stay <-> move

        //TopFly
        randRange = 3f;
        fTime = 0.5f;
        StartCoroutine(checkPosi(randRange));

        //Spider
        currTime = attaackSpeed;
        spdierMoveTime = 3f;
    }
    private void Update()
    {
        if (playerInRoom)
        {
            Move();
        }

    }

    public override void Move()
    {
        //���� ��ȭ
        currTime += Time.deltaTime;
        if (currTime < attaackSpeed)
        {
            ChageState(SpiderState.SpiderStay);
        }
        else if (currTime < spdierMoveTime)
        {

            ChageState(SpiderState.SpiderRandMove);
        }
        else
        {
            // Ÿ�̸Ӹ� �缳���ϰ� "SpiderStay" ���·� ���ư�
            animator.SetBool("isMove", false);
            currTime = 0;
            ChageState(SpiderState.SpiderStay);
        }
    }

    void ChageState(SpiderState newState)
    {
        // ���� ���� ����
        StopCoroutine(state.ToString());
        // ���ο� ���·� ����
        state = newState;
        // ���� ������ �ڷ�ƾ ����
        StartCoroutine(state.ToString());
    }

    IEnumerator SpiderStay()
    {
        while (true)
        {
            //�ƹ��͵� ����
            yield return null;
        }
    }
    IEnumerator SpiderRandMove()
    {
        animator.SetBool("isMove", true);
        while (true)
        {
            Prwol();
            yield return null;
        }
    }

}
