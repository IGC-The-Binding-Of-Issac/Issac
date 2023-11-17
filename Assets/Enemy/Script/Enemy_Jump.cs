using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Enemy_Jump : TEnemy_FSM<TEnemy>
{
    [SerializeField] TEnemy e_Owner;                          // ���� ����

    float playerToDis;
    float jumpAiPlayTime;
    Vector3 myPosi;

    public Enemy_Jump(TEnemy _ower)                       // ������ �ʱ�ȭ
    {
        e_Owner = _ower;
    }

    public override void Enter()
    {
        e_Owner.eCurState = TENEMY_STATE.Jump;              // ���� ���¸� TENEMY_STATE�� Tracking���� 
        e_Owner.e_findPlayer();                             // �÷��̾��� ��ġ�� 1ȸ �޾ƿ�

        e_Owner.e_moveInialize();                           // �� ��ġ 1ȸ �޾ƿ���
        e_Owner.e_findPlayer();                             // �÷��̾� ��ġ 1ȸ �޾ƿ���
        myPosi = new Vector3(e_Owner.getMyx, e_Owner.getMyy, 0); 
        playerToDis = Vector3.Distance(e_Owner.playerPosi.transform.position, myPosi);
        jumpAiPlayTime = playerToDis / e_Owner.getJumpSpeed;    // (����)�ð� = �÷��̾�� tride�Ÿ� / ���� �ӵ�
    }

    public override void Excute()
    {
        if (e_Owner.e_isDead())                                     // ���Ͱ� ������ 
        {
            e_Owner.ChageFSM(TENEMY_STATE.Die);                     // Die�� ���º�ȭ 
        }
    }

    public override void Exit()
    {
        e_Owner.ePreState = TENEMY_STATE.Jump;
    }

}
