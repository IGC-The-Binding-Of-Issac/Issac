using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [Header("Sprite")]
    public Sprite defaultTearImg;
    SpriteRenderer bodyRenderer;
    SpriteRenderer headRenderer;
    Rigidbody2D playerRB;

    [Header("Function")]
    private float lastshot;
    Vector2 moveInput;

    float shootHor;
    float shootVer;
    public GameObject tear;

    [Header("Unity Setup")]
    public TearPoint tearPoint;

    [Header("State")]
    public GameObject CheckedObject;

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRB = GetComponent<Rigidbody2D>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        headRenderer = head.GetComponent<SpriteRenderer>();
        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = defaultTearImg;
    }

    void Update()
    {
        MoveAnim();
        ShotAnim();
        InstallBomb();
    }
    void FixedUpdate()
    {
        Movement();
    }

    //�̵� ���
    void Movement()
    {
        float moveSpeed = PlayerManager.instance.playerMoveSpeed;
        float shotDelay = PlayerManager.instance.playerShotDelay;

        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");
        moveInput = hori * Vector2.right + verti * Vector2.up;
        //�밢 �̵��ӵ� 1 �ѱ��� �ʱ�
        if(moveInput.magnitude > 1f)
        {
            moveInput.Normalize();
        }
        shootHor = Input.GetAxis("ShootHorizontal");
        shootVer = Input.GetAxis("ShootVertical");

        //�Ѿ� �߻� ������
        if ((shootHor != 0 || shootVer != 0) && Time.time > lastshot + shotDelay)
        {
            if (shootHor != 0 && shootVer != 0)
            {
                //�밢 �߻� X
                shootHor = 0;
            }
            Shoot(shootHor, shootVer);
            lastshot = Time.time;
        }

        //�÷��̾� ������
        playerRB.velocity = moveInput * moveSpeed;
    }

//�߻� ���
public void Shoot(float x, float y)
    {
        float tearSpeed = PlayerManager.instance.playerTearSpeed;
        Vector3 firePoint = gameObject.transform.GetChild(5).transform.position;
        //�߻� ��� ����
        //���� �� ���� ���� ���� ������, �߻� ������ġ, ȸ��
        tear = Instantiate(PlayerManager.instance.tearObj, firePoint, transform.rotation) as GameObject;
        tear.GetComponent<Rigidbody2D>().velocity = new Vector3(x * tearSpeed, y * tearSpeed, 0);

        
        CheckedObject = null;
        if(y != 1) // �� ������ �ƴҶ�
        {
            CheckedObject = tearPoint.overLapObject;
        }

        //�Ѿ��� �밢���� �з��� �߻�ǰ� ������ ���� ��
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
            //���� �߻��� �� ���� ���̾� ���̱�
        }
        if (Input.GetKey(KeyCode.S))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.down * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.left * 1.5f, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.right * 1.5f, ForceMode2D.Impulse);
        }
    }

    //�̵� �ִϸ��̼�
    void MoveAnim()
    {
        if (moveInput.x < 0) { bodyRenderer.flipX = true; }
        if (moveInput.x > 0) { bodyRenderer.flipX = false; }
        playerMoveAnim.SetFloat("Up&Down", Mathf.Abs(moveInput.y));
        playerMoveAnim.SetFloat("Left&Right", Mathf.Abs(moveInput.x));
        if (Input.GetKey(KeyCode.W))
        {
            playerShotAnim.SetBool("UpLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
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
        if (Input.GetKeyDown(KeyCode.A))
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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
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
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        playerAnim.SetTrigger("Hit");
        int randomIndex = Random.Range(0, audioClips.Length);

        audioSource.clip = audioClips[randomIndex];
        audioSource.Play();
    }

    // ��� �ִϸ��̼�
    public void Dead()
    {
        //player head, player body ������Ʈ ã�Ƽ� ����
        head.gameObject.SetActive(false);
        body.gameObject.SetActive(false);
        playerAnim.SetTrigger("Death");

        // ����ִϸ��̼� ���� ����ƿ�Ʈ�� ������ �̵� �ۼ�
    }

    //������ ȹ�� �ִϸ��̼�
    public IEnumerator GetItemTime()
    {
        //���� ����� ������
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        //������ ȹ�� �ִϸ��̼� ����
        getItem.SetTrigger("GetItem");
        //�ִϸ��̼� 1�ʰ� ����
        yield return new WaitForSeconds(1f);
        //�÷��̾� ��� ���̰� ��
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);

        //itemPosition �ڽ��� �����
        if (itemPosition.childCount != 0)
        {
            Destroy(itemPosition.GetChild(0).gameObject);
        }
    }

    public IEnumerator GetTrinketItem()
    {
        //���� ����� ������
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);
        //������ ȹ�� �ִϸ��̼� ����
        getItem.SetTrigger("GetItem");
        //�ִϸ��̼� 1�ʰ� ����
        yield return new WaitForSeconds(1f);
        //�÷��̾� ��� ���̰� ��
        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
        ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().KeepItem();
    }

    public IEnumerator GetActiveItem()
    {
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(1f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);

        ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>().KeepItem();
    }

    public IEnumerator UseActiveItem()
    {
        headRenderer.color = new Color(1, 1, 1, 0);
        bodyRenderer.color = new Color(1, 1, 1, 0);

        getItem.SetTrigger("GetItem");
        yield return new WaitForSeconds(0.4f);

        headRenderer.color = new Color(1, 1, 1, 1);
        bodyRenderer.color = new Color(1, 1, 1, 1);
    }

    //��ź ��ġ ���
    void InstallBomb()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            // �ʵ忡 ��ġ�� ��ź�̾����� && �������� ��ź ������ 1�� �̻��϶�
            if(GameObject.Find("Putbomb") == null && ItemManager.instance.bombCount > 0)
            {
                ItemManager.instance.bombCount--;
                GameObject bomb = Instantiate(ItemManager.instance.bombPrefab, transform.position, Quaternion.identity) as GameObject;
                bomb.name = "Putbomb"; // ������ ��ź ������Ʈ �̸� ����
            }
        }
    }


}