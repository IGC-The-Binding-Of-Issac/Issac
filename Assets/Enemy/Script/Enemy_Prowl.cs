using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Prowl : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // ���� ����

    public Enemy_Prowl(TEnemy _ower)                        // ������ �ʱ�ȭ
    {
        e_Owner = _ower;
    }

    public override void Enter()                            // �ش� ���¸� ������ �� "1ȸ" ȣ��
    {
        //Debug.Log(e_Owner.gameObject.tag + " : Prowl ���� ");
        e_Owner.eCurState = TENEMY_STATE.Prowl;          // ���� ���¸� TENEMY_STATE�� Prowl  
    }

    public override void Excute()                           // �ش� ���¸� ������Ʈ �� �� "�� ������" ȣ��
    {

    }

    public override void Exit()                              // �ش� ���¸� ������ �� "1ȸ" ȣ��
    {        
        e_Owner.ePreState = TENEMY_STATE.Prowl;              // �� ���¸� TENEMY_STATE�� Prowl
    }
}
