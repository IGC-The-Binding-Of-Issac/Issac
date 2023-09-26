using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Sprite destoryRock;
    // ��ź�� �ǰ��� DestoryRock()�� ȣ��.
    public void DestroyRock()
    {
        dropItem();
        gameObject.GetComponent<SpriteRenderer>().sprite = destoryRock;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    void dropItem()
    {
        // ItemManage���� ������ ���� ����غ��ô�.
        //����,ü�� �� ��������� Ȯ�����. ���� �ʿ�.
    }
}
