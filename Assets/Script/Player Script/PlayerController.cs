using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator playerMoveAnim;
    public Animator playerShotAnim;
    public Animator playerAnim;
    public Animator getItem;
    public Transform itemPosition;

    Rigidbody2D playerRB;

    public GameObject tearPrefab;
    GameObject tear;

    float tearSpeed;
    float moveSpeed;
    float shotDelay;
    private float lastshot;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �̺κ� ���� �����ؾ��ҵ�
        //�� ������ ����Ǵϱ� �ڿ������� ��ư ������
        moveSpeed = PlayerManager.instance.playerMoveSpeed;
        tearSpeed = PlayerManager.instance.playerTearSpeed;
        shotDelay = PlayerManager.instance.playerShotDelay;


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
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");

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
        playerRB.velocity = new Vector3(hori * moveSpeed, verti * moveSpeed, 0);
    }

    //�߻� ���
    void Shoot(float x, float y)
    {
        //�߻� ��� ����
        //���� �� ���� ���� ���� ������, �߻� ������ġ, ȸ��
        tear = Instantiate(tearPrefab, transform.position + Vector3.up * 0.4f, transform.rotation) as GameObject;
        tear.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * tearSpeed : Mathf.Ceil(x) * tearSpeed,
            (y < 0) ? Mathf.Floor(y) * tearSpeed : Mathf.Ceil(y) * tearSpeed, 0);

        //�Ѿ��� �밢���� �з��� �߻�ǰ� ������ ���� ��
        if (Input.GetKey(KeyCode.W))
        {
            Rigidbody2D rigid_bullet = tear.GetComponent<Rigidbody2D>();
            rigid_bullet.AddForce(Vector2.up * 1.5f, ForceMode2D.Impulse);
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            playerMoveAnim.SetBool("playerFrontWalk", true);
        }
        else
        {
            playerMoveAnim.SetBool("playerFrontWalk", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerMoveAnim.SetBool("playerLeftWalk", true);
        }
        else
        {
            playerMoveAnim.SetBool("playerLeftWalk", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerMoveAnim.SetBool("playerRightWalk", true);
        }
        else
        {
            playerMoveAnim.SetBool("playerRightWalk", false);
        }
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
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        playerAnim.SetTrigger("Hit");
    }

    // ��� �ִϸ��̼�
    public void Dead()
    {
        //player head, player body ������Ʈ ã�Ƽ� ����
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        playerAnim.SetTrigger("Death");

        // ����ִϸ��̼� ���� ����ƿ�Ʈ�� ������ �̵� �ۼ�
    }

    //������ ȹ�� �ִϸ��̼�
    public IEnumerator GetItemTime(bool isTrinket)
    {
        //���� ����� ������
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //������ ȹ�� �ִϸ��̼� ����
        getItem.SetTrigger("GetItem");
        //�ִϸ��̼� 1�ʰ� ����
        yield return new WaitForSeconds(1f);
        //�÷��̾� ��� ���̰� ��
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        //itemPosition �ڽ��� ����� isTrinket=false�� ����
        if (itemPosition.childCount != 0)
        {
            Destroy(itemPosition.GetChild(0).gameObject);
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
                ItemManager.instance.bombCount--;
                GameObject bomb = Instantiate(ItemManager.instance.bombPrefab, transform.position, Quaternion.identity) as GameObject;
                bomb.name = "Putbomb"; // ������ ��ź ������Ʈ �̸� ����
            }
        }
    }
}