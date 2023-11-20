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
    public int CanBlockDamage = 0; // Holy Mantle ���� �� �������� 3ȸ ������ش�.
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
    public SpriteLibraryAsset[] headClothes;
    public SpriteLibraryAsset[] bodyClothes;

    public void Start()
    {
        ItemManager.instance.bombPrefab.transform.localScale = new Vector3(1, 1, 1);
        gameObject.AddComponent<AudioSource>();
        PlayerInitialization();
    }
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
    #region SkinChange
    public void CheckedShotDelay()
    {
        if(playerShotDelay < 0.025)
        {
            playerShotDelay = 0.025f;
        }
    }

    public void SetHeadSkin(int index)
    {
        //�Ӹ� ������ �ٲٴ� �Լ�
        ChangeHead(head[index]);
    }
    public void SetBodySkin(int index) 
    { 
        //���� ������ �ٲٴ� �Լ�
        ChangeBody(body[index]);
    }
    public void SetTearSkin(int index)
    {
        //���� ������ �ٲٴ� �Լ�
        ChangeTear(tear[index]);
    }
    public void SetHeadClothesSkin(int index)
    {
        //���� ������ �ٲٴ� �Լ�
        ChangeHeadClothes(headClothes[index]);
    }
    public void SetBodyClothesSkin(int index)
    {
        //�� ������ �ٲٴ� �Լ�
        ChangeBodyClothes(bodyClothes[index]);
    }
    public void ChangeHead(SpriteLibraryAsset head)
    {
        //SpriteLibraryAsset�ȿ� �ִ� head�� �÷��̾� ��������Ʈ�� �־��� 
        GameManager.instance.playerObject.GetComponent<PlayerController>().head.GetComponent<SpriteLibrary>().spriteLibraryAsset = head;
    }
    public void ChangeHeadClothes(SpriteLibraryAsset headClothes)
    {
        //SpriteLibraryAsset�ȿ� �ִ� headClothes�� �÷��̾� ��������Ʈ�� �־��� 
        GameManager.instance.playerObject.GetComponent<PlayerController>().headClothes.GetComponent<SpriteLibrary>().spriteLibraryAsset = headClothes;
    }

    public void ChangeBody(SpriteLibraryAsset body)
    {
        //SpriteLibraryAsset�ȿ� �ִ� body�� �÷��̾� ��������Ʈ�� �־���
        GameManager.instance.playerObject.GetComponent<PlayerController>().body.GetComponent<SpriteLibrary>().spriteLibraryAsset = body;
    }
    public void ChangeBodyClothes(SpriteLibraryAsset bodyClothes)
    {
        //SpriteLibraryAsset�ȿ� �ִ� bodyClothes�� �÷��̾� ��������Ʈ�� �־���
        GameManager.instance.playerObject.GetComponent<PlayerController>().bodyClothes.GetComponent<SpriteLibrary>().spriteLibraryAsset = bodyClothes;
    }

    public void ChangeTear(SpriteLibraryAsset tear)
    {
        //SpriteLibraryAsset�ȿ� �ִ� tear�� �÷��̾� ��������Ʈ�� �־���
        Transform allChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().tearPointTransform;
        for (int i = 0; i < allChildren.childCount; i++)
        {
            GameObject obj = allChildren.GetChild(i).gameObject;
            obj.GetComponent<SpriteLibrary>().spriteLibraryAsset = tear;
        }
        //tearObj.GetComponent<SpriteLibrary>().spriteLibraryAsset = tear;
    }
    #endregion
    //delegate ���� ��ġ


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

        //�ǰ� �� �����̴� ȿ��
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
            //0.1�ʿ� �ѹ��� ������
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
        //���� ������ ����
        Transform tearChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().tearPointTransform;
        Transform DrbombChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().DrBombPointTransform;
        for (int i = 0; i < tearChildren.childCount; i++)
        {
            GameObject tearObj = tearChildren.GetChild(i).gameObject;
            tearObj.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
        }
        for (int i = 0; i < DrbombChildren.childCount; i++)
        {
            GameObject bombObj = DrbombChildren.GetChild(i).gameObject;
            bombObj.transform.localScale = new Vector3(playerTearSize, playerTearSize, 0);
        }
    }

    public void ChgPlayerSize()
    {
        //�÷��̾� ������ ����
        playerObj = GameManager.instance.playerObject;
        playerObj.transform.localScale = new Vector3(playerSize, playerSize, 0);
    }
}
