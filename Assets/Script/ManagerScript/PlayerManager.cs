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

    GameObject playerObj;
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
            Hit();
            playerObj.transform.GetChild(0).gameObject.SetActive(true);
            playerObj.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(hitDelay);
        CanGetDamage = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        Dead();
        }
    }
    void Hit()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.transform.GetChild(0).gameObject.SetActive(false);
        playerObj.transform.GetChild(1).gameObject.SetActive(false);
        playerObj.GetComponent<PlayerController>().HitAnim();        
    }

    // ��� �Լ�
    void Dead()
    {
        //player head, player body ������Ʈ ã�Ƽ� ����
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.transform.GetChild(0).gameObject.SetActive(false);
        playerObj.transform.GetChild(1).gameObject.SetActive(false);
        // ����ִϸ��̼� ����
        playerObj.GetComponent<PlayerController>().DieAnim();
        // ����ִϸ��̼� ���� ����ƿ�Ʈ�� ������ �̵� �ۼ�
    }
    
    void GetItemAnim()
    {
        //������ �Դ� �ִϸ��̼�
    }
}
