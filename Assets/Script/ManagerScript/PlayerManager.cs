using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.U2D.Animation;

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

    public bool CanGetDamage = true; // �������� ���� �� �ִ��� Ȯ��.
    public int CanBlockDamage = 0; // Holy Mantle ���� �� �������� 5ȸ ������ش�.
    public int deathCount = 0;
    float hitDelay = .5f; // �ǰ� ������

    [Header("unity setup")]
    public GameObject tearObj;
    GameObject playerObj;

    [Header("Player Sprite")]
    SpriteRenderer playerHead;
    SpriteRenderer playerBody;
    SpriteRenderer headItem;

    [Header("Player OutFit")]
    public SpriteLibraryAsset[] head;
    public SpriteLibraryAsset[] body;
    public SpriteLibraryAsset[] tear;

    void PlayerInitialization()
    {
        playerHp = 6; // ���� ü��
        playerMaxHp = 6; // �ִ� ü��
        playerMoveSpeed = 5f; // �̵� �ӵ�
        playerTearSpeed = 6f; // ����ü �ӵ�
        playerShotDelay = 0.5f; // ���� ������
        playerDamage = 1f; // ������
        playerRange = 5f; // ��Ÿ�
        playerTearSize = 1f; //���� ũ��
        playerSize = 1f; //ĳ���� ũ��
        CanBlockDamage = 0;
        CanGetDamage = true;
        hitDelay = 0.5f; // �ǰ� ������
    }

    public void CheckedShotDelay()
    {
        if(playerShotDelay < 0.025)
        {
            playerShotDelay = 0.025f;
        }
    }

    public void SetHeadSkin(int index)
    {
        ChangeHead(head[index]);
    }
    public void SetBodySkin(int index) 
    { 
        ChangeBody(body[index]);
    }
    public void SetTearSkin(int index)
    {
        ChangeTear(tear[index]);
    }
    public void ChangeHead(SpriteLibraryAsset head)
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().head.GetComponent<SpriteLibrary>().spriteLibraryAsset = head;
    }

    public void ChangeBody(SpriteLibraryAsset body)
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().body.GetComponent<SpriteLibrary>().spriteLibraryAsset = body;
    }

    public void ChangeTear(SpriteLibraryAsset tear)
    {
        tearObj.GetComponent<SpriteLibrary>().spriteLibraryAsset = tear;
    }

    //delegate ���� ��ġ

    public void Start()
    {
        tearObj.transform.localScale = new Vector3(1, 1, 1);
        ItemManager.instance.bombPrefab.transform.localScale = new Vector3(1, 1, 1);
        gameObject.AddComponent<AudioSource>();
        PlayerInitialization();
        SetTearSkin(0);
    }

    public void CheckedPlayerHP()
    {
        if (playerHp<=0)
        {
            GameManager.instance.playerObject.GetComponent<PlayerController>().Dead();
            Invoke("GameOver", 0.7f);
        }
        else if (playerHp > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
    }

    public void GetDamage()
    {
        if (ItemManager.instance.PassiveItems[6] && CanGetDamage && CanBlockDamage > 0) //Ȧ�� ��Ʋ �Ծ��� ��
        {
            StartCoroutine(HitDelay());
            CanGetDamage = false;
            CanBlockDamage--;
        }
        else if (ItemManager.instance.PassiveItems[14] && CanGetDamage && CanBlockDamage == 0) // Dead Cat �Ծ��� ��
        {
            playerHp--;
            CanGetDamage = false;
            if (playerHp > 0)
            {
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
            }
            else //ü���� 0�� ��
            {
            int[] liveOrDeath = { 0, 1 }; 
            int randomNum = (UnityEngine.Random.Range(0, 100000) % 2);
            if (liveOrDeath[randomNum] == 0) 
                 {
                    playerHp = 2; //�ִ� ü�¸�ŭ ä����
                    StartCoroutine(HitDelay());
                    GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
                 }
            else
                 {
                    GameManager.instance.playerObject.GetComponent<PlayerController>().Dead(); //�׳� ����
                    Invoke("GameOver", 0.7f);
                 }
            }
        }
        else if (ItemManager.instance.PassiveItems[15] && CanGetDamage && CanBlockDamage == 0) // Guppy's tail �Ծ��� ��
        {
            playerHp--;
            CanGetDamage = false;
            if (playerHp > 0)
            {
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
            }
            else if (playerHp <= 0 && deathCount >= 1)
            {
                playerHp = playerMaxHp;
                StartCoroutine(HitDelay());
                GameManager.instance.playerObject.GetComponent<PlayerController>().Hit();
                deathCount--;
            }
            else
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Dead(); //�׳� ����
                Invoke("GameOver", 0.7f);
            }
        }
        else if (CanGetDamage && CanBlockDamage == 0)
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
    public IEnumerator HitDelay()
    {
        playerObj = GameManager.instance.playerObject;
        playerHead = playerObj.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        playerBody = playerObj.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        headItem = playerObj.transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>();
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
