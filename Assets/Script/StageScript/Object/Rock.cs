using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Sprite destoryRock;

    // ��ź�� �ǰ��� DestoryRock()�� ȣ��.
    public void DestroyRock()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = destoryRock;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        DestorySound();
    }

    void DestorySound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
