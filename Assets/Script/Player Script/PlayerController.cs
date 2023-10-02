using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator PlayerMoveAnim;

    public Animator PlayerShotAnim;

    public Animator PlayerDieAnim;

    public SpriteRenderer flip;

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
        moveSpeed = PlayerManager.instance.playerMoveSpeed;
        tearSpeed = PlayerManager.instance.playerTearSpeed;
        shotDelay = PlayerManager.instance.playerShotDelay;
    }

    void Update()
    {
        Movement();

        MoveAnim();

        ShotAnim();

        InstallBomb();
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
        tear = Instantiate(tearPrefab, transform.position + Vector3.up * 0.4f, transform.rotation) as GameObject;
        tear.AddComponent<Rigidbody2D>().gravityScale = 0;
        tear.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * tearSpeed : Mathf.Ceil(x) * tearSpeed,
            (y < 0) ? Mathf.Floor(y) * tearSpeed : Mathf.Ceil(y) * tearSpeed, 0);

        //�Ѿ��� �밢���� ������ ������ ���� ��
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
            PlayerMoveAnim.SetBool("playerFrontWalk", true);
        }
        else
        {
            PlayerMoveAnim.SetBool("playerFrontWalk", false);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                flip.flipX = true;
            }
            PlayerMoveAnim.SetBool("playerSideWalk", true);
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                flip.flipX = false;
            }
            PlayerMoveAnim.SetBool("playerSideWalk", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            PlayerShotAnim.SetBool("UpLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShotAnim.SetBool("UpLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShotAnim.SetBool("UpLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("UpLook", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerShotAnim.SetBool("DownLook", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerShotAnim.SetBool("DownLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShotAnim.SetBool("DownLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("DownLook", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerShotAnim.SetBool("LeftLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerShotAnim.SetBool("LeftLook", false);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerShotAnim.SetBool("LeftLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("LeftLook", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerShotAnim.SetBool("RightLook", true);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerShotAnim.SetBool("RightLook", false);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerShotAnim.SetBool("RightLook", false);
            }
        }
        else
        {
            PlayerShotAnim.SetBool("RightLook", false);
        }
    }

    //�߻� �ִϸ��̼�
    void ShotAnim()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerShotAnim.SetBool("playerLeftShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerLeftShot", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerShotAnim.SetBool("playerRightShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerRightShot", false);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            PlayerShotAnim.SetBool("playerUpShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerUpShot", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            PlayerShotAnim.SetBool("playerDownShot", true);
        }
        else
        {
            PlayerShotAnim.SetBool("playerDownShot", false);
        }
    }

    //��� �ִϸ��̼�
    public void DieAnim() 
    {        
        PlayerDieAnim.SetTrigger("Death");
    }

    public void HitAnim()
    {
        PlayerDieAnim.SetTrigger("Hit");
    }
    void HitMotion()
    {
        transform.localScale += new Vector3(-0.3f, 0.3f, 0);
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

    // ������ ���� ���� ���� �ʿ�.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            PlayerManager.instance.playerHp--;
        }
    }
}