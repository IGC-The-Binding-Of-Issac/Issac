using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Top_Spider : Enemy
{
    //Spider �̵�
    [HideInInspector]
    protected float x;
    protected float y;
    protected float xRan;
    protected float yRan;

    // ���� ������ ���� �� �� �ʿ���! + �ڵ�ƾ ����
    protected float randRange; //�������� �̵��� ���� 
    protected float fTime; //�����̵� �ð�
    
    // ������ Tracking!
    protected Transform justTrackingPlayerPosi; // ������ ���� ���� update������ player �ǽð� �޾ƿ���
    protected bool isFlipped;

    // Top_issac , Top_Fly �� public void Lookplayer()
    // Spider�� Ư�� public void Lookplayer(Transform fp) : fp�� �޾ƿ�

    protected void Spider_Move_InitialIze()
    {

        // enemy �ʱ� ��ġ
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        xRan = x;
        yRan = y;

        isFlipped = false;
    }

    // �÷��̾� ����
    // 1. �����ȿ� ������ �� searching �� tracking -> fp : enemy���� �˻��� playerPosi
    // 2. �׳� ������ tracking -> fp : �������� update���� �ִ� justTrackingPlayerPosi
    protected void Tracking(Transform fp)
    {
        if (fp == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, fp.transform.position, moveSpeed * Time.deltaTime);
    }


    //���� ������

    protected void Prwol()
    {
        Vector3 moveRan = new Vector3(xRan, yRan, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveRan, moveSpeed * Time.deltaTime);
    }

    //���� ������ �˻�
    protected IEnumerator checkPosi(float randRange)
    {
        while (true)
        {
            yield return new WaitForSeconds(fTime);
            // x��ġ�� ���� ��ġ randRange���� , ������ġ -randRange����
            // y��ġ ����
            xRan = Random.Range(x + randRange, x - randRange);
            yRan = Random.Range(y + randRange, y - randRange);
        }
    }

    // ���� �ø�
    public void Lookplayer(Transform fp)
    {
        if (transform.position.x > fp.position.x && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < fp.position.x && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

}
