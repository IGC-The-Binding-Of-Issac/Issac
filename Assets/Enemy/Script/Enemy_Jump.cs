using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Jump : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // ���� ����

    public Enemy_Jump(TEnemy _ower)                       // ������ �ʱ�ȭ
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        e_Owner.eCurState = TENEMY_STATE.Jump;          // ���� ���¸� TENEMY_STATE�� Tracking���� 
    }

    public override void Excute()
    {

    }

    public override void Exit()
    {
        e_Owner.ePreState = TENEMY_STATE.Jump;
    }
}
