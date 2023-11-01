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
    public int playerMaxHp = 6; // �ִ� ü��
    public float playerMoveSpeed = 5f; // �̵� �ӵ�
    public float playerTearSpeed = 6f; // ����ü �ӵ�
    public float playerShotDelay = 0.5f; // ���� ������
    public float playerDamage = 1f; // ������
    public float playerRange = 5f; // ��Ÿ�
    public float playerTearSize = 1f; //���� ũ��
    public float playerSize = 1f; //ĳ���� ũ��

    bool CanGetDamage = true; // �������� ���� �� �ִ��� Ȯ��.
    float hitDelay = .5f; // �ǰ� ������

    [Header("unity setup")]
    public GameObject tearObj;
    GameObject playerObj;

    [Header("Player Sprite")]
    SpriteRenderer playerHead;
    SpriteRenderer playerBody;
    SpriteRenderer headItem;

    //delegate ���� ��ġ
    
    public void Start()
    {
        tearObj.transform.localScale = new Vector3(1, 1, 1);
        gameObject.AddComponent<AudioSource>();
    }
    public void GetDamage()
    {
        if(CanGetDamage)
        {
            playerHp--;
            CanGetDamage = false;
            if(playerHp <= 0) // �������� �޾����� HP�� 0���ϰ� �Ǹ� ����Լ� ����.
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
                Invoke("GameOver", 0.7f);
            }
            else
            {
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
            }
        }            
    }

    void GameOver()
    {
        Invoke("GameOver", 0.7f);
        UIManager.instance.GameOver();
    }

    //�ǰ� ������
    IEnumerator HitDelay()
    {
        playerObj = GameManager.instance.playerObject;
        playerHead = playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        playerBody = playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        headItem = playerObj.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>();
        //�ǰ� ���ڸ�ŭ ������
        yield return new WaitForSeconds(hitDelay);

        int countTime = 0;

        while(countTime < 14)
        {
            //countTIme%2 == 0�̸� �÷��̾� ����� ����
            if (countTime%2 == 0)
            {
                playerHead.color = new Color(1, 1, 1, 1);
                playerBody.color = new Color(1, 1, 1, 1);
                headItem.color = new Color(1, 1, 1, 1);
            }
            //countTIme%2 != 0�̸� �÷��̾� ����� �Ⱥ���
            else
            {
                playerHead.color = new Color(1, 1, 1, 0);
                playerBody.color = new Color(1, 1, 1, 0);
                headItem.color = new Color(1, 1, 1, 0);
            }
            countTime++;

            yield return new WaitForSeconds(0.1f);
        }
        //while�� ���� �� ����� ����
        playerHead.color = new Color(1, 1, 1, 1);
        playerBody.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);

        //�ǰ� ���� ��
        CanGetDamage = true;
    }
    public void ChgTearSize()
    {
        tearObj.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
    }

    public void ChgPlayerSize()
    {
        playerObj = GameManager.instance.playerObject;
        playerObj.transform.localScale = new Vector3(playerSize, playerSize, 0);
    }
}
