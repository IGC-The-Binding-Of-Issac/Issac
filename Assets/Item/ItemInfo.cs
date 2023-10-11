using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    protected int itemCode;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // �������� ����������������.
            if(!ItemManager.instance.PassiveItems[itemCode])
            {
                ItemManager.instance.PassiveItems[itemCode] = true; // �̺��� -> ���� �� ���� 
                UseItem();

                gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(gameObject.GetComponent<Rigidbody2D>());

                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetItemTime());
            }
        }
    }

    public virtual void UseItem() { Debug.Log("������ ����!"); }

    public void SetItemCode(int code)
    {
        itemCode = code;
    }
}
