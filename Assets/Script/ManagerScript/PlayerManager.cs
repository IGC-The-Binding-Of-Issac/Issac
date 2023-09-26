using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Player Stat")]
    public int playerHp = 24; // ���� ü��
    public int playerMaxHp = 24; // �ִ�ü��
    public float playerMoveSpeed = 5f; // �̵��ӵ�
    public float playerTearSpeed = 6f; // ����ü�ӵ�
    public float playerShotDelay = 0.5f; // ���ݵ�����
    public float playerDamage = 1f; // ������
    public float playerRange = 5f; // ��Ÿ�



    
    //�� ���� �� �ִ� ü���� ���� ���ϰ� ��
    public void HealPlayer(int healAmount)
    {
        playerHp = Mathf.Min(playerMaxHp, playerHp + healAmount);
    }
}
