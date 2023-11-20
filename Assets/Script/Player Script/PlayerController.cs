
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Animation")]
    public Animator playerMoveAnim;
    public Animator playerShotAnim;
    public Animator playerAnim;
    public Animator getItem;

    [Header("Transform")]
    public Transform itemPosition;
    public Transform body;
    public Transform head;
    public Transform useActiveItemImage;
    public Transform knifePosition;
    public Transform familiarPosition;
    public Transform tearPointTransform;
    public Transform bombPointTransform;

    [Header("Sprite")]
    SpriteRenderer bodyRenderer;
    SpriteRenderer headRenderer;
    SpriteRenderer headItem;
    Rigidbody2D playerRB;
    public Sprite tearDefaultSprite;
    public Sprite bombDefaultSprite;

    [Header("Function")]
    private float lastshot;
    Vector2 moveInput;
    float shootHor;
    float shootVer;

    [Header("PlayerState")]
    public GameObject HeadItem;
    public GameObject CheckedObject;
    public GameObject tear;
    public GameObject bomb;
    public TearPoint tearPoint;
    GameObject DefaultTearObject;

    [Header("ItemState")]
    public GameObject knife;
    public bool nailActivated; // ��� �������� ������� ��
    public bool canUseActive = true; //��Ƽ�� ������ �������� �����ϱ� ����
    public bool canChangeItem = false; //��Ƽ�� ������ ���� �����ϸ� ����

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] hitClips;
    public AudioClip[] deadClips;
    public AudioClip getItemClip;
    public AudioClip ShootClip;

    Stack<GameObject> tearPool;
    Stack<GameObject> bombPool;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody2D>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        headRenderer = head.GetComponent<SpriteRenderer>();
        headItem = GameManager.instance.playerObject.transform.GetChild(6).gameObject.GetComponent<SpriteRenderer>();

        canUseActive = true; // ��Ƽ�� ������ �������� �����ϱ� ����
        canChangeItem = true; // ��Ƽ�� ������ ���� �����ϸ� ����
        nailActivated = false;
        tearPool = new Stack<GameObject>();
        bombPool = new Stack<GameObject>();
        SetTearPooling();
        SetBombPooling();
        //knifePosition.gameObject.SetActive(false);
    }

    void Update()
    {
        MoveAnim();
        ShotAnim();
        InstallBomb();
        UseActive();
    }
    void FixedUpdate()
    {
        Movement();
    }
    #region bombPooling

    public void SetBombPooling()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject bombObj = Instantiate(bomb, bombPointTransform.position, Quaternion.identity);
            bombPool.Push(bombObj);
            bombObj.transform.SetParent(bombPointTransform.transform);
            bombObj.gameObject.SetActive(false);
        }
    }
    public GameObject GetBombPooling()
    {
        if (bombPool.Count == 0)
        {
            GameObject bombObj = Instantiate(bomb, bombPointTransform.position, Quaternion.identity);
            bombPool.Push(bombObj);
            bombObj.transform.SetParent(bombPointTransform.transform);
            bombObj.gameObject.SetActive(false);
        }
        GameObject bombObject = bombPool.Pop();
        bombObject.SetActive(true);
        return bombObject;
    }
    public void ReturnBombPooling(GameObject bombObj)
    {
        bombObj.GetComponent<SpriteRenderer>().sprite = bombDefaultSprite;
        bombObj.transform.localPosition = Vector3.zero;
        bombObj.SetActive(false);
        bombObj.GetComponent<PutBomb>().CanAttack = false;
        bombObj.GetComponent<BoxCollider2D>().offset = new Vector2(0.04f, -0.03f);
        bombObj.GetComponent<BoxCollider2D>().size = new Vector2(0.6f, 0.64f);
        bombPool.Push(bombObj);
    }
    #endregion

    #region tearPooling
    public void SetTearPooling()
    {
        for (int i = 0; i < 40; i++)
        {
            GameObject tearObj = Instantiate(tear, tearPointTransform.position, Quaternion.identity);
            tearPool.Push(tearObj);
            tearObj.transform.SetParent(tearPoint.transform);
            tearObj.gameObject.SetActive(false);
        }
    }
    public GameObject GetTearPooling()
    {
        if (tearPool.Count == 0)
        {
            GameObject tearObj = Instantiate(tear, tearPointTransform.position, Quaternion.identity);
            tearPool.Push(tearObj);
            tearObj.transform.SetParent(tearPoint.transform);
            tearObj.gameObject.SetActive(false);
        }
        GameObject tearObject = tearPool.Pop();
        tearObject.SetActive(true);
        tearObject.GetComponent<Tear>().SetPlayerPosition(transform.position);
        return tearObject;
    }

    public void ReturnTearPooling(GameObject bullet)
    {
        bullet.GetComponent<SpriteRenderer>().sprite = tearDefaultSprite;
        //bullet.transform.localScale = new Vector3(1, 1, 1);
        bullet.transform.localPosition = Vector3.zero;
        bullet.GetComponent<CircleCollider2D>().enabled = true;
        bullet.GetComponent<SpriteRenderer>().sortingOrder = 103;
        bullet.SetActive(false);
        tearPool.Push(bullet);
    }
    #endregion

    #region UseActiveItem
    //��Ƽ�� ������ ���
    void UseActive()
    {
        // �������� �ְ�, �����̽��� ��������
        if (ItemManager.instance.ActiveItem != null && canUseActive)
        {
            if (ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().canUse)
            {
                ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().CheckedItem();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ActiveInfo active = ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>();

                    if (active.currentEnergy >= active.needEnergy) // �ʿ� ������ �Ѿ�����.
                    {
                        if (active.activeItemCode == 1 && ItemManager.instance.coinCount <= 0) return;
                        else
                        {
                            StartCoroutine(UseActiveItem()); // ������ ��� �ִϸ��̼�
                            active.UseActive();  // ������ ��� ����
                            if (active.activeItemCode == 0)
                            {
                                nailActivated = true;
                            }
                        }
                        active.currentEnergy = 0;
                        canUseActive = false;
                        Invoke("SetActiveDelay", 1f);
                        Invoke("SetCanChangeItem", 1f);
                    }
                }
            }
        }
    }

    void SetActiveDelay()
    {
        canUseActive = true;
    }
    
    void SetCanChangeItem()
    {
        canChangeItem = true;
    }
    #endregion

    #region PlayerFunction
    //�̵� ���
    void Movement()
    {
        //�̵��ӵ�
        float moveSpeed = PlayerManager.instance.playerMoveSpeed;
        //�߻� ������
        float shotDelay = PlayerManager.instance.playerShotDelay;

        //���� �̵� Ű�Է�
        float hori = Input.GetAxis("Horizontal");
        //���� �̵� Ű�Է�
        float verti = Input.GetAxis("Vertical");
        //�Է����� �� �̵� ���� ����
        moveInput = hori * Vector2.right + verti * Vector2.up;
        //�밢 �̵��ӵ� 1 �ѱ��� �ʱ�
        if(moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }
        //���� �߻� Ű �Է�
        shootHor = Input.GetAxis("ShootHorizontal");
        //���� �߻� Ű �Է�
        shootVer = Input.GetAxis("ShootVertical");

        if (ItemManager.instance.PassiveItems[13] && !ItemManager.instance.PassiveItems[16])
        {
            KnifeAttack(hori,verti,shootHor, shootVer);
        }
        else
        {
            //�Ѿ� �߻� ������ ������ �߻�� �����̸� ���� ����ð�(�ʴ���)�� �Ѿ�� ����
            if ((shootHor != 0 || shootVer != 0) && Time.time > lastshot + shotDelay)
            {
                //���γ� ���� �Է��� ���� �� ��
                if (shootHor != 0 && shootVer != 0)
                {
                    //�밢 �߻� X
                    shootHor = 0;
                }
                //�Ѿ��� �������� �׸��� ������
                if (ItemManager.instance.PassiveItems[9])
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        //����Ű�� ������ �� �밢 �� 55���� ���� �ִ� �ڵ�
                        Shoot(Mathf.Cos(55 * Mathf.Deg2Rad), Mathf.Sin(55 * Mathf.Deg2Rad));
                    }
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        //����Ű�� ������ �� �밢 �� 120���� ���� �ִ� �ڵ�
                        Shoot(Mathf.Cos(120 * Mathf.Deg2Rad), Mathf.Sin(120 * Mathf.Deg2Rad));
                    }
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        Shoot(shootHor, shootVer);
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        Shoot(shootHor, shootVer);
                    }
                }
                else
                {
                    Shoot(shootHor, shootVer);
                }
                //������ �߻翡 ���� �ð�(�ʴ���)�� ����
                lastshot = Time.time;
            }
        }
        //�÷��̾� ������
        playerRB.velocity = moveInput * moveSpeed;
    }
    //�߻� ���
    public void Shoot(float x, float y)
    {
        //���� �߻� �ӵ�
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        //���� ���� ����
        Vector3 firePoint = tearPoint.transform.position;

        if (ItemManager.instance.PassiveItems[16])
        {
            DefaultTearObject = DrFetusBomb();
        }
        else
        {
            DefaultTearObject = GetTearPooling();
        }

        DefaultTearObject.transform.position = firePoint;
        //������ ������ �����ӵ� ���ؼ� ���ֱ�
        DefaultTearObject.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);

        //9�� �нú� �������� ������
        if (ItemManager.instance.PassiveItems[9])
        {
            //���� �߷� ����
            tear.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        else
            tear.GetComponent<Rigidbody2D>().gravityScale = 0;

        CheckedObject = null;
        if (y != 1) // �� ������ �ƴҶ�
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //������ �̵��ӵ��� ������ ����
        //�ش� �̵�Ű�� ������ 
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            //�ش� �������� ���� ��
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
        //2�� �нú� �������� ������
        if (ItemManager.instance.PassiveItems[2])
        {
            //�ش� �Լ� 4�� ����
            for (int i = 0; i < 3; i++)
                MutantShoot(x, y);
        }
    }
    public void MutantShoot(float x, float y)
        {
        //���� �߻� �ӵ�
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        //���� ���� ����
        Vector3 firePoint = tearPoint.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

        if (ItemManager.instance.PassiveItems[16])
        {
            DefaultTearObject = DrFetusBomb();
        }
        else
        {
            DefaultTearObject = GetTearPooling();
        }

        DefaultTearObject.transform.position = firePoint;
        //������ ������ �����ӵ� ���ؼ� ���ֱ�
        DefaultTearObject.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);

        //9�� �нú� �������� ������
        if (ItemManager.instance.PassiveItems[9])
        {
            //���� �߷� ����
            tear.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        else
            tear.GetComponent<Rigidbody2D>().gravityScale = 0;

        CheckedObject = null;
        if (y != 1) // �� ������ �ƴҶ�
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //������ �̵��ӵ��� ������ ����
        //�ش� �̵�Ű�� ������ 
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            //�ش� �������� ���� ��
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = DefaultTearObject.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
    }

    //���� �ִϸ��̼� Ŭ�� �̺�Ʈ�� �������� �߰� �Ǿ�����

    public void KnifeAttack(float moveX, float moveY, float shootX, float shootY)
    {
        if(knife.GetComponent<KnifeObject>().canShoot)
        {
            if (shootX > 0) //������
            {
                knifePosition.localPosition = new Vector3(0.94f, 0.2f, 0);
                knife.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (shootX < 0) //����
            {
                knifePosition.localPosition = new Vector3(-0.84f, 0.2f, 0);
                knife.transform.rotation = Quaternion.Euler(180, 0, 90);
            }
            if (shootY > 0) //��
            {
                knifePosition.localPosition = new Vector3(0, 1.31f, 0);
                knife.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (shootY < 0) //�Ʒ�
            {
                knifePosition.localPosition = new Vector3(0, -0.69f, 0);
                knife.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }
    //��ź ��ġ ���
    void InstallBomb()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // �ʵ忡 ��ġ�� ��ź�̾����� && �������� ��ź ������ 1�� �̻��϶�
            if(GameObject.Find("Putbomb") == null && ItemManager.instance.bombCount > 0)
            {
                GameObject bomb = GetBombPooling();
                bomb.GetComponent<PutBomb>().PlayerBomb();
                ItemManager.instance.bombCount--;

                bomb.name = "Putbomb"; // ������ ��ź ������Ʈ �̸� ����
            }
        }
    }

    public GameObject DrFetusBomb()
    {
        GameObject bomb = GetBombPooling();
        bomb.GetComponent<PutBomb>().PlayerBomb();
        return bomb;
    }
    #endregion

    #region PlayerAnim
    //�̵� �ִϸ��̼�
    void MoveAnim()
    {
        //�����Է¹��� -> -1 ���� �ִϸ��̼� ������(����)
        if (moveInput.x < 0) { bodyRenderer.flipX = true; }
        //�����Է¹��� -> 1 ���� �ִϸ��̼� �״��(������)
        if (moveInput.x > 0) { bodyRenderer.flipX = false; }
        //�����Է� ����
        playerMoveAnim.SetFloat("Up&Down", Mathf.Abs(moveInput.y));
        //�����Է� ����
        playerMoveAnim.SetFloat("Left&Right", Mathf.Abs(moveInput.x));

        //���ΰ��� ����Ű�� ������ ��
        if (Input.GetKey(KeyCode.W))
        {
            //���� ���� �ִϸ��̼� ����
            playerShotAnim.SetBool("UpLook", true);
            //�ٸ� �߻� Ű�� ������
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //�ִϸ��̼� ����
                playerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerShotAnim.SetBool("UpLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("UpLook", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerShotAnim.SetBool("DownLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerShotAnim.SetBool("DownLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("DownLook", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerShotAnim.SetBool("LeftLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerShotAnim.SetBool("LeftLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("LeftLook", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerShotAnim.SetBool("RightLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerShotAnim.SetBool("RightLook", false);
            }
        }
        else
        {
            playerShotAnim.SetBool("RightLook", false);
        }
    }

    //�߻� �ִϸ��̼�
    void ShotAnim()
    {
        //�� �߻� ����Ű�� ������
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //�ش� ���� �ִϸ��̼� ����
            playerShotAnim.SetBool("playerLeftShot", true);
        }
        else
        {
            playerShotAnim.SetBool("playerLeftShot", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerShotAnim.SetBool("playerRightShot", true);
        }
        else
        {
            playerShotAnim.SetBool("playerRightShot", false);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerShotAnim.SetBool("playerUpShot", true);
            //���� �� �� ���� ���̾� ���� �ٲ�
            Transform allChildren = GameManager.instance.playerObject.GetComponent<PlayerController>().tearPointTransform;
            for (int i = 0; i < allChildren.childCount; i++)
            {
                GameObject obj = allChildren.GetChild(i).gameObject;
                obj.GetComponent<SpriteRenderer>().sortingOrder = 101;
            }
        }
        else
        {
            playerShotAnim.SetBool("playerUpShot", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            playerShotAnim.SetBool("playerDownShot", true);
        }
        else
        {
            playerShotAnim.SetBool("playerDownShot", false);
        }
    }

    //�ǰ� �ִϸ��̼�
    public void Hit()
    {
        //�Ӹ� ����
        headRenderer.color = new Color(1, 1, 1, 0);
        //���� ����
        bodyRenderer.color = new Color(1, 1, 1, 0);
        //�Ӹ��� ������ ������ ����
        headItem.color = new Color(1, 1, 1, 0);
        //�ǰ� �ִϸ��̼� ����
        playerAnim.SetTrigger("Hit");
        //�ǰ� ���� ����
        HitSound();
    }

    // ��� �ִϸ��̼�
    public void Dead()
    {
        //��� ���� ����
        DeadSound();

        //player head, player body ������Ʈ ã�Ƽ� ����
        head.gameObject.SetActive(false);
        body.gameObject.SetActive(false);
        //��� �ִϸ��̼� ����
        playerAnim.SetTrigger("Death");
    }
    #endregion

    #region GetItemAnim
    //������ ȹ�� �ִϸ��̼�
    public IEnumerator GetItemTime()
    {
        //���� head, body,headitem ����� ������
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);
        //������ ȹ�� �ִϸ��̼� ����
        getItem.SetTrigger("GetItem");
        //�ִϸ��̼� 1�ʰ� ����
        yield return new WaitForSeconds(1f);
        //�÷��̾� ��� ���̰� ��
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);

        //itemPosition �ڽ��� �����
        if (itemPosition.childCount != 0)
        {
            Destroy(itemPosition.GetChild(0).gameObject);
        }
    }

    public IEnumerator GetTrinketItem()
    {
        GetitemSound();
        //���� ����� ������
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);
        //������ ȹ�� �ִϸ��̼� ����
        getItem.SetTrigger("GetItem");
        //�ִϸ��̼� 1�ʰ� ����
        yield return new WaitForSeconds(1f);
        //�÷��̾� ��� ���̰� ��
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);
        ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().KeepItem();
    }

    public IEnumerator GetActiveItem()
    {
        GetitemSound();
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(1f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);
        ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().KeepItem();
    }

    public IEnumerator UseActiveItem()
    {
        Sprite activeSpr = ItemManager.instance.ActiveItem.GetComponent<SpriteRenderer>().sprite;
        useActiveItemImage.GetComponent<SpriteRenderer>().sprite = activeSpr;
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        headItem.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(1f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        headItem.color = new Color(1, 1, 1, 1);
        useActiveItemImage.GetComponent<SpriteRenderer>().sprite = null;
    }
    #endregion

    #region Sound
    public void HitSound()
    {
        int randomIndex = Random.Range(0, hitClips.Length);

        audioSource.clip = hitClips[randomIndex];
        audioSource.Play();
    }
    public void DeadSound()
    {
        int randomIndex = Random.Range(0, deadClips.Length);

        audioSource.clip = deadClips[randomIndex];
        audioSource.Play();
    }

    public void GetitemSound()
    {
        audioSource.clip = getItemClip;
        audioSource.Play();
    }
    #endregion
}