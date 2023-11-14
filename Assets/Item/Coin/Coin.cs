using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator coin_animator;
    private Rigidbody2D rb;
    private CapsuleCollider2D cl;

    public AudioSource audioSource;

    public AudioClip pickupClip;
    public AudioClip dropClip;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        float randomX = Random.Range(-1.0f, 1.0f);
        float randomY = Random.Range(-1.0f, 1.0f);
        float randomForce = Random.Range(50f, 70f);
        rb.AddForce(new Vector2(randomX, randomY) * randomForce);
        coin_animator = GetComponent<Animator>();
        cl = GetComponent<CapsuleCollider2D>();
        cl.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 31; // �÷��̾�� �浹�����ʴ� ���̾�
            coin_animator.SetTrigger("GetCoin");
            ItemManager.instance.coinCount++;
        }
    }

    public void GetCoin()
    {
        audioSource.volume = SoundManager.instance.GetSFXVolume();
        audioSource.clip = pickupClip;
        audioSource.Play();

        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(this.gameObject,0.5f);
    }
    public void DropCoinEnd()
    {
        coin_animator.SetBool("DropCoin", false);
        cl.enabled = true;
        audioSource.volume = SoundManager.instance.GetSFXVolume();
        audioSource.clip = dropClip;
        audioSource.Play();
    }
}
