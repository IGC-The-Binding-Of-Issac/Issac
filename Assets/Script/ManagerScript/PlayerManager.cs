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

    public GameObject playerGameObj;
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dead();
        }
    }

    // ��� �Լ�
    void Dead()
    {
        //player head, player body ������Ʈ ã�Ƽ� ����
        playerGameObj = GameObject.FindGameObjectWithTag("Player");
        playerGameObj.transform.GetChild(0).gameObject.SetActive(false);
        playerGameObj.transform.GetChild(1).gameObject.SetActive(false);
        // ����ִϸ��̼� ����
        playerGameObj.GetComponent<PlayerController>().DieAnim();
        // ����ִϸ��̼� ���� ����ƿ�Ʈ�� ������ �̵� �ۼ�
    }
    
    void GetItemAnim()
    {
        //������ �Դ� �ִϸ��̼�
    }
}
