using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy m_Owner;

    //생성자 초기화
    public Enemy_Shoot(TEnemy _ower)
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
