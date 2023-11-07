using MyFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZOMBIE_STATE 
{
    Idle,
    Walk,
    Die, // �����̰� �̷��Ŵ� �θ𿡼� �ۼ� �ؾ��ϴµ�
    Attack
}


public class Zombie_FSM : MonoBehaviour
{
    /// <summary>
    /// 
    ///  1. FSM�� ������Ʈ�� �پ� �ӽ��� ����
    ///  2. State�� ��� �����ϴ���
    /// 
    /// </summary>

    // �ӽſ� �� �����̵�
    public Head_Machine<Zombie_FSM> m_state;

    // ���� �̸� ������ ���� state
    // System.Enum.GetValues(typeof(state�̸�)).Length : enum Ÿ���� ���� ���ϱ�
    // ���⼱ [4]
    public FSM<Zombie_FSM>[] m_arrState = new FSM<Zombie_FSM>[System.Enum.GetValues(typeof(ZOMBIE_STATE)).Length];


    public float m_findRange;     // ���� �ٸ� ������Ʈ�� ã�� ����
    public Transform m_TransTarget; //  ���� ã�� Ÿ��

    public int m_iHealth; // ������ ü��
    public float m_fAttackRange; // ������ ���� ����

    public ZOMBIE_STATE m_eCurState; // ������ ���� ���� 
    public ZOMBIE_STATE m_ePrevState; // ������ ���� ���� 

    public Animator m_Animator;

    // ������
    public Zombie_FSM() 
    {
        init();
    }

    // �ʿ� ������ �ʱ�ȭ
    public void init()
    {
        m_state = new Head_Machine<Zombie_FSM>();

        //m_arrState[(int)ZOMBIE_STATE.Idle] = new Zombie_Idle(this); // ������ �ش� ��ũ��Ʈ �ۼ� ������
        //m_arrState[(int)ZOMBIE_STATE.Walk] = new Zombie_Walk(this);
        //m_arrState[(int)ZOMBIE_STATE.Die] = new Zombie_Die(this);
        //m_arrState[(int)ZOMBIE_STATE.Attack] = new Zombie_Attack(this);

        m_state.SetState(m_arrState[(int)ZOMBIE_STATE.Idle] , this); // Head_Machine�� SetState
        // ���� ������Ʈ(this)�� idle�� �ٲ۴�
    }

    // ���� ��ȭ
    // �Ű������� ���� state�� ���¸� �ٲ۴� (�迭�� ����Ǿ��ִ� ���¸� ��)
    public void ChageFSM(ZOMBIE_STATE ps) 
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(ZOMBIE_STATE)).Length; i++) 
        {
            if (i == (int)ps)
                m_state.Change(m_arrState[(int)ps]);
        }
    }

    public void Begin() 
    {
        m_state.Begin(); //Zombie_FSM������ Head_Machine�� begin ���� 
    }

    public void Run()
    {
        m_state.Run(); //`` Head_Machine�� Run ���� 
    }

    public void Exit() 
    {
        m_state.Exit(); //`` Head_Machine�� Exit ���� 
    }
    
    // Awake
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Begin();
    }

    private void Update()
    {
        Run();

        // �� �ð� �߰� �ϴ� �ڵ�..��¼��..
        
    }
}
