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
    public int playerHp = 6; // ���� ü��
    public int playerMaxHp = 6; // �ִ�ü��
    public float playerMoveSpeed = 5f; // �̵��ӵ�
    public float playerTearSpeed = 6f; // ����ü�ӵ�
    public float playerShotDelay = 0.5f; // ���ݵ�����
    public float playerDamage = 1f; // ������
    public float playerRange = 5f; // ��Ÿ�


    bool CanGetDamage = true; // �������� ���� �� �ִ��� Ȯ��.
    float hitDelay = 1f; // �ǰ� ������
    public void GetDamage()
    {
        if(CanGetDamage)
        {
            playerHp--;
            CanGetDamage = false;
            StartCoroutine(HitDelay());
            if(playerHp <= 0) // �������� �޾����� HP�� 0���ϰ� �Ǹ� ����Լ� ����.
            {
                Dead();
            }
            HitAnim();
        }
    }
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(hitDelay);
        CanGetDamage = true;
    }
    void HitAnim()
    {
        // �ǰ� �ִϸ��̼� �ۼ�
    }

    // ��� �Լ�
    void Dead()
    {
        // ����ִϸ��̼� �ۼ�
        //player head, player body ������Ʈ ���� ��� �ִϸ��̼� ����
        // ����ִϸ��̼� ���� ����ƿ�Ʈ�� ������ �̵� �ۼ�
    }
    
    void GetItemAnim()
    {
        //������ �Դ� �ִϸ��̼�
    }
}
