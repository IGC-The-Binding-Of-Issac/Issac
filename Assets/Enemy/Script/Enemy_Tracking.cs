using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Tracking : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // ���� ����
    bool coruState;      
    Coroutine runningCoroutine = null;                        // �ڷ�ƾ 

    public Enemy_Tracking(TEnemy _ower)                       // ������ �ʱ�ȭ
    {
        e_Owner = _ower;
    }

    public override void Enter()                            // �ش� ���¸� ������ �� "1ȸ" ȣ��
    {
        //Debug.Log(e_Owner.gameObject.tag + " : Tracking ���� ");
        e_Owner.eCurState = TENEMY_STATE.Tracking;          // ���� ���¸� TENEMY_STATE�� Tracking���� 

        coruState = true;
    }

    public override void Excute()                                   // �ش� ���¸� ������Ʈ �� �� "�� ������" ȣ��
    {
        e_Owner.e_findPlayer();                                     // player Ž��
        e_Owner.e_Tracking();                                       // tracking
       
        if (e_Owner.trackingTarget == null)
            return;

        if (e_Owner.e_isDead())                                     // ���Ͱ� ������ 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die�� ���º�ȭ 
        }

        // ���� ���� �ϴ¾ָ�?
            // -> �� ����� �ƴ����� ���� �޶���
        if (e_Owner.getisDetective)                                
        {
            
            if (e_Owner.e_SearchingPlayer())                         // sight ���� �ȿ� ������
            {
                if (e_Owner.getisShoot)                             // �� ��� �ָ�?
                {
                    e_Owner.ChageFSM(TENEMY_STATE.Shoot);           // Shoot���� ���� ��ȭ
                }
            }
            else if (!e_Owner.e_SearchingPlayer())                  // ���� �ۿ� ������
            {
                e_Owner.ChageFSM(TENEMY_STATE.Prowl);               // prowl�� ���� ��ȭ
            }
        }

        

    }

    public override void Exit()                              // �ش� ���¸� ������ �� "1ȸ" ȣ��
    {
        e_Owner.ePreState = TENEMY_STATE.Tracking;              // �� ���¸� TENEMY_STATE�� Tracking
    }

    // �� ��� ���·� �Ѿ�� �� ��Ÿ��
    /*
    IEnumerator waitShoot()
    {
        yield return new WaitForSeconds(5f);
        e_Owner.IsReadyShoot = true;                    // �� ��� ������ true��
        e_Owner.ChageFSM(TENEMY_STATE.Shoot);           // �� ��� ����
    }
    */

}
