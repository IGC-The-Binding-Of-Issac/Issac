using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrinketInfo : MonoBehaviour
{
    public int trinketItemCode;
    public string itemTitle; //������ �̸�
    public string itemDescription; //������ ��� ���� [���� �� �߾� UI �ؿ� �ؽ�Ʈ ����]
    public string itemInformation; // ������ ���� [���� �� ���� UI�� �����]
    public bool canCollision = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹 ����� �÷��̾��϶�
        if(collision.gameObject.CompareTag("Player") && canCollision
            && GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem)
        {
            gameObject.layer = 31;

            // 1. ��ű� �������� �����ϰ� ���� ���� ��
            if (ItemManager.instance.TrinketItem == null)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                TrinketGet(collision);
            }
            
            // 2. ��ű� �������� �����ϰ� ���� ��
            else if(ItemManager.instance.TrinketItem != null)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
                // ���� �������� �������� ȿ���� ����.
                ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().DropTrinket();

                // ���� �������� �������� ����
                GameObject obj = ItemManager.instance.itemTable.TrinketChange(ItemManager.instance.TrinketItem.GetComponent<TrinketInfo>().trinketItemCode);
                Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
                GameObject beforeTrinket = Instantiate(obj, new Vector3(dropPosition.position.x, (dropPosition.position.y - 1f), 0),Quaternion.identity) as GameObject;

                // ���� ����� ������ ����Ʈ�� ���.
                GameManager.instance.roomGenerate.itemList.Add(beforeTrinket);

                // �����ִ� ������ ����.
                Destroy(ItemManager.instance.TrinketItem);
                TrinketGet(collision);
            }
            Invoke("SetCanChangeItem", 1f);
        }
    }

    public void SetTrinketItemCode(int code)
    {
        trinketItemCode = code;
    }

    public void SetCanChangeItem()
    {
        GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = true;
    }
    public void SetTrinketString(string title, string description, string information)
    {
        itemTitle = title;
        itemDescription = description;
        itemInformation = information;
    }
    public void KeepItem() 
    {
        transform.position = ItemManager.instance.itemStorage.position;
        transform.SetParent(ItemManager.instance.itemStorage);
    }

    public virtual void GetItem() { 
        Debug.Log("������ ����!"); 
    }

    public virtual void DropTrinket()
    {
        Debug.Log("��ű� ����߸� �� ������");
    }
    private void TrinketChange(Collision2D collision)
    {
        ItemManager.instance.TrinketItem = this.gameObject;
        DisconnectTrinket();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);

        Destroy(gameObject.GetComponent<Rigidbody2D>());
        StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetTrinketItem());

    }
    private void TrinketGet(Collision2D collision)
    {
        TrinketChange(collision);
        GetItem();
    }

    void DisconnectTrinket()
    {
        /*
         ���� ���� �����ɶ� �������� Trinket�� ���� �����Ǵ������� �ذ��ϱ�����
         ���� �������� Trinket�� �� ���� ��ũ��Ʈ�� itemList���� ���������ν� ( �濡 ����Ǵ� ��� �����۵��� ItemList�� �� )
         ���� �� �����ɶ� ( �������� �Ѿ�� )  �������� ��ű��� �ƹ��� ������ �������ϵ��� ������.
        */
        // roomGenerete�� itemList���� ���ܽ�Ŵ.
        for (int i = 0; i < GameManager.instance.roomGenerate.itemList.Count; i++)
        {
            if (ItemManager.instance.TrinketItem.Equals(GameManager.instance.roomGenerate.itemList[i]))
            {
                GameManager.instance.roomGenerate.itemList.RemoveAt(i);
            }
        }
    }

    void SetDelay()
    {
        canCollision = true;
    }
    private void Update()
    {
        if (!canCollision)
            Invoke("SetDelay", 0.8f);
    }
}
