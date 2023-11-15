using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Tracking : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // ���� ����

    public Enemy_Tracking(TEnemy _ower)                       // ������ �ʱ�ȭ
    {
        e_Owner = _ower;
    }

    public override void Enter()                            // �ش� ���¸� ������ �� "1ȸ" ȣ��
    {
        Debug.Log(e_Owner.gameObject.tag + " : Tracking ���� ");
        e_Owner.eCurState = TENEMY_STATE.Tracking;          // ���� ���¸� TENEMY_STATE�� Tracking���� 
    }

    public override void Excute()                                   // �ش� ���¸� ������Ʈ �� �� "�� ������" ȣ��
    {
        e_Owner.e_findPlayer();                                     // player Ž��

        if (e_Owner.trackingTarget == null)
            return;

        if (e_Owner.getIsTracking)                                  // Tracking �ϴ� enemy  �̸�
        {
            e_Owner.e_Tracking();                                   // tracking
        }

        if (e_Owner.getisProwl && !e_Owner.e_SearchingPlayer())      // prowl�� �ϴ� enemy + �÷��̾ ���� �ȿ� ���� �� 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Prowl);                    // prowl�� ���� ��ȭ
        }
        if (e_Owner.getisProwl && e_Owner.e_SearchingPlayer())       // prowl�� �ϴ� enemy + �÷��̾ ���� �ȿ� ������
        {
            e_Owner.e_Tracking();                                    // tracking
        }   
    }

    public override void Exit()                              // �ش� ���¸� ������ �� "1ȸ" ȣ��
    {
        e_Owner.ePreState = TENEMY_STATE.Tracking;              // �� ���¸� TENEMY_STATE�� Tracking
    }


}
