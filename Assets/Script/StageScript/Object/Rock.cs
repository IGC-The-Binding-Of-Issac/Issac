using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Sprite destoryRock;
    Sprite defaultSprite;
    // ��ź�� �ǰ��� DestoryRock()�� ȣ��.
    public void DestroyRock()
    {
        defaultSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        gameObject.GetComponent<SpriteRenderer>().sprite = destoryRock;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        DestorySound();
    }

    void DestorySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void ResetObject()
    {
        // �ʱ�ȭ
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        // ������Ʈ ����.
        gameObject.SetActive(false);
    }
}
