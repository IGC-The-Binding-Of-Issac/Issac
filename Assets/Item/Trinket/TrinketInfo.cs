using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class TrinketInfo : MonoBehaviour
{
    public bool isTrinket;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && ItemManager.instance.TrinketItem == null)
        {
            isTrinket = true;
            ItemManager.instance.TrinketItem = this.gameObject;
            gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            //StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetItemTime(isTrinket));
            //������ �����ϴ� �ڷ�ƾ/�ִϸ��̼� ���� �� transform�� �Ʒ��� ī�޶��� localPosition���� �Űܾ���.
            GetItem();
            transform.SetParent(GameManager.instance.myCamera.transform);
            transform.localPosition = new Vector3(-6, -4, 0);
        }
    }

    public virtual void GetItem() { 
        Debug.Log("������ ����!"); 
    }
}
