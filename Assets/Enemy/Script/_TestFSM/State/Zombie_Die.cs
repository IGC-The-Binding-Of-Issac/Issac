using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Die : FSM<Zombie_FSM>
{
    // ������ Attack ���� ����
    // �� ��ũ��Ʈ�� ���� ��ų ��ũ��Ʈ ���� (Zombie_FSM��ũ��Ʈ, �� ����)
    [SerializeField] Zombie_FSM m_Owner;

    //������ �ʱ�ȭ
    public Zombie_Die(Zombie_FSM _ower)
    {
        m_Owner = _ower;
    }

    public override void Begin()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Run()
    {
        throw new System.NotImplementedException();
    }
}
