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
    float hitDelay = .5f; // �ǰ� ������

    public void Start()
    {

    }
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
            }else
                Hit();
        }            
    }

    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(hitDelay);

        int countTime = 0;

        while(countTime < 10)
        {
            if (countTime%2 == 0)
            {
                playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
            countTime++;

            yield return new WaitForSeconds(0.1f);
        }
        playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        CanGetDamage = true;
    }        

    void Hit()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        playerObj.GetComponent<PlayerController>().HitAnim();

        //playerObj.transform.GetChild(0).gameObject.SetActive(false);
        //playerObj.transform.GetChild(1).gameObject.SetActive(false);
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
