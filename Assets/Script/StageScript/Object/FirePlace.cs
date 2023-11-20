using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    int fireIndex = -1;

    AudioSource sfxAudio;
    [Header("Unity SetUp")]
    [SerializeField] Sprite woodSprite;
    [SerializeField] GameObject eft;
    [SerializeField] GameObject boxCollider;
    [SerializeField] AudioClip destoryClip;
    [SerializeField] AudioClip fireClip;


    Sprite defaultSprite;
    Vector3 defaultScale;
    private void Start()
    {
        sfxAudio = GetComponent<AudioSource>();
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        defaultScale = eft.transform.localScale;
    }

    public void ResetObject()
    {
        //�ʱ�ȭ
        fireIndex = -1;
        FireSound();
        eft.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        GetComponent<BoxCollider2D>().enabled = true;
        eft.transform.localScale = defaultScale;

        //������Ʈ ����
        gameObject.SetActive(false);
    }

    public void GetDamage() 
    {
        fireIndex++;
        ChangeEffectSize();
        if(fireIndex >= 3)
        {
            DestorySound();

            ItemDrop();
            eft.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().sprite = woodSprite;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            boxCollider.SetActive(false);
        }
    }

    private void ChangeEffectSize()
    {
        eft.transform.localScale = new Vector3(eft.transform.localScale.x - 0.2f, eft.transform.localScale.y - 0.2f, 0);
    }
    void ItemDrop()
    {
        int rd = Random.Range(0, 3);
        if (rd == 0)
        {
            ItemManager.instance.itemTable.Dropitem(transform.position,rd);
        }
    }

    void DestorySound()
    {
        AudioSource objectAudio = gameObject.GetComponent<AudioSource>();
        objectAudio.Stop();
        objectAudio.loop = false;
        objectAudio.clip = destoryClip;
        objectAudio.Play();
    }

    void FireSound()
    {
        AudioSource objectAudio = gameObject.GetComponent<AudioSource>();
        objectAudio.loop = true;
        objectAudio.clip = fireClip;
        objectAudio.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.instance.GetDamage();
        }
    }
}
