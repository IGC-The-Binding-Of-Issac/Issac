using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFSM 
{
    public class Zombie_Idle : FSM<Zombie_FSM> // <���� ��ũ��Ʈ�� ���� �� ��ũ��Ʈ �̸�>
    {
        // ������ Idle ���� ����
        // �� ��ũ��Ʈ�� ���� ��ų ��ũ��Ʈ ���� (Zombie_FSM��ũ��Ʈ, �� ����)
        [SerializeField] Zombie_FSM m_Owner;

        //������ �ʱ�ȭ
        public Zombie_Idle(Zombie_FSM _ower)
        {
            m_Owner = _ower;
        }

        // ���� begin
        public override void Begin()
        {
            Debug.Log("Zombie_Idle : �⺻ ����");

            // Zombie_FSM��ũ��Ʈ�� enum�� ZOMBIE_STATE�� idle�� �ٲ�
            // �������(Cur)�� �ٲ�
            m_Owner.m_eCurState = ZOMBIE_STATE.Idle;
        }

        // ���� ����
        public override void Run()
        {
            Debug.Log("Zombie_Idle : �⺻ ����");

            //m_Owner.m_TransTarget = null; // �θ� ������ m_TransTarget�� ������ �� ����
            if (FindRange()) 
            {
                m_Owner.ChageFSM(ZOMBIE_STATE.Walk);
                // ���ǿ� �����ϸ� ���� ����            
            }

            // idle �� ������ �Ǹ� = idle ������ �ϸ�
            // ZOMBIE_STATE��  walk�� Fsm �� ��ȯ�Ѵ�

        }

        // ���� ��
        public override void Exit()
        {
            Debug.Log("Zombie_Idle : �⺻ ��");

            // Zombie_FSM��ũ��Ʈ�� enum�� ZOMBIE_STATE�� idle�� �ٲ�
            // �������(Pre)�� �ٲ�
            m_Owner.m_ePrevState = ZOMBIE_STATE.Idle;

        }

        // ã�� �Լ�
        private bool FindRange() 
        {
            // � �ڵ带 �ֵ���
            // m_Owner.m_TransTarget �� �θ��� Target�� ã���� �ɵ�

            return true;
        }
    }

}
