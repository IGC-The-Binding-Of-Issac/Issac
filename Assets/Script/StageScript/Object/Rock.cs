using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Obstacle
{
    [SerializeField] Sprite[] rockSprite;

    protected override void initialization()
    {
        GetComponent<SpriteRenderer>().sprite = rockSprite[spriteIndex];
        objectLayer = 8;
    }

    public override void ResetObject()
    {
        // �ʱ�ȭ
        spriteIndex = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = rockSprite[0];
        gameObject.layer = objectLayer;

        // ������Ʈ ����.
        gameObject.SetActive(false);
    }

    public override void Returnobject()
    {
        // �ش� ������Ʈ�� Ǯ stack�� �־��ֱ�
    }

    public override void GetDamage()
    {
        spriteIndex++;
        ChangeObjectSPrite();
        gameObject.layer = noCollisionLayer;

        DestorySound();
    }

    protected override void ChangeObjectSPrite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = rockSprite[spriteIndex];
    }
}
