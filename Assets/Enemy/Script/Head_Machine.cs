using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFSM 
{
    public class Head_Machine<T>
    {
        // ������Ʈ
        private T Owner;

        // ���� ��
        private FSM<T> m_CurState = null; // ���� ����
        private FSM<T> m_PrevState = null; // ���� ����

        // ó�� ���°�
        public void Begin() // Head_Machine�� �޼���??
        {
            if (m_CurState != null) //ó�� ���°� null �� �ƴϸ�? 
            {
                m_CurState.Begin();
            }
        }

        // ���� ������Ʈ
        public void Run() 
        {
            CheckState();
        }

        // ���� üũ
        public void CheckState() 
        {
            if (m_CurState != null) 
            {
                m_CurState.Run();    // Run ���·� �ٲٴ�?
            }
        }

        // fsm ����
        public void Exit() 
        {
            m_CurState.End(); // ������ m_CurState.Exit()�ε� ������!!
            m_CurState = null;
            m_PrevState = null;
        
        
        }

        public void Change(FSM<T> _state)
        {
            //���� �����̸� ����
            if (_state == m_CurState)
                return;

            m_PrevState = m_CurState; // ���� ���¸� ���� ���·� ??

            // ���� ���°� �ִٸ� ����
            if (m_CurState != null)
                m_CurState.End();

            m_CurState = _state;
            // ���� ����� ���°� null�� �ƴϸ� ����
            if (m_CurState != null)
                m_CurState.Begin();
        }

        //��ȭ�� �� �ƹ��� ���ڰ��� ������ ���� ���°� ���
        public void Revert() 
        {
            if (m_PrevState != null)
                Change(m_CurState);
        }
        
        // ���°� ����
        public void SetState(FSM<T> _state, T _Owner)
        {
            Owner = _Owner;
            m_CurState = _state;

            // ���� ���°��� ���ݰ� �ٸ��� && ������°��� ä���� ���� ��
            if (m_CurState != _state && m_CurState != null)
                m_PrevState = m_CurState;
        }

    }

}
