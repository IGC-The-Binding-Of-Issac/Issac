using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Die : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy m_Owner;

    //������ �ʱ�ȭ
    public Enemy_Die(TEnemy _ower)
    {
        m_Owner = _ower;
    }

    public override void Enter()
    {
        
    }

    public override void Excute()
    {
        
    }

    public override void Exit()
    {
        
    }


}
