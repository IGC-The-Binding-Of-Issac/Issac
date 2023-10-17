using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class TrinketInfo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 ����� �÷��̾��϶�
        if(collision.gameObject.CompareTag("Player"))
        {
            // 1. ��ű� �������� �����ϰ� ���� ���� ��
            if (ItemManager.instance.TrinketItem == null)
            {
                ItemManager.instance.TrinketItem = this.gameObject;
                gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetAcTrItem());
            }
            // 2. ��ű� �������� �����ϰ� ���� ��
            else if(ItemManager.instance.TrinketItem != null)
            {
                // �ڵ� �ۼ� �ʿ�.
            }
        }
    }

    public void KeepItem() 
    {
        transform.position = ItemManager.instance.itemStorage.position;
        transform.SetParent(ItemManager.instance.itemStorage);
    }

    public virtual void GetItem() { 
        Debug.Log("������ ����!"); 
    }
}
