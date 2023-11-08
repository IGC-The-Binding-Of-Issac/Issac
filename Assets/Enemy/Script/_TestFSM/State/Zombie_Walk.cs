using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM 
{
    public class Zombie_Walk : FSM<Zombie_FSM>
    {
        // ������ walk ���� ����
        // �� ��ũ��Ʈ�� ���� ��ų ��ũ��Ʈ ���� (Zombie_FSM��ũ��Ʈ, �� ����)
        [SerializeField] Zombie_FSM m_Owner;
        Animator m_Animator;

        //������ �ʱ�ȭ
        public Zombie_Walk(Zombie_FSM _ower)
        {
            m_Owner = _ower;
        }

        public override void Begin()
        {
            Debug.Log("Zombie_Walk : �ȱ� ����");
            // ���� ���¸� walk ���·�
            m_Owner.m_eCurState = ZOMBIE_STATE.Walk;

            m_Animator = m_Owner.m_Animator; // �θ��� �ִϸ����� 
            // m_Animator.SetBool("Walk", true);
        }

        public override void Run()
        {
            Debug.Log("Zombie_Walk : �ȱ� ����");

            // ����
            if(m_Owner.m_TransTarget != null) 
            {
                Tracnking();
            }
            // ���� ���� �ȿ� ������ 
            if (m_Owner.m_TransTarget != null && m_Owner.m_fAttackRange >= 10f) 
            {
                // ZOMBIE_STATE�� Attack ���·� ��ȯ
                m_Owner.ChageFSM(ZOMBIE_STATE.Attack);
            }
            
        }

        public override void Exit()
        {
            Debug.Log("Zombie_Walk : �ȱ� ��");
            // �� ���¸� walk ���·�
            m_Owner.m_ePrevState = ZOMBIE_STATE.Walk;

            // m_Animator.SetBool("Walk", false); // �ִϸ��̼� ����
        }

        public void Tracnking()
        {
            // walk ���� �� �� �����ϴ� �ڵ� �ֱ�
        }
    }
}

