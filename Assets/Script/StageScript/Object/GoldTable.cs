using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTable : MonoBehaviour
{
    GameObject roomInfoObject;
    Room roomInfo;

    [Header("State")]
    [SerializeField] GameObject item;

    [Header("Unity Setup")]
    [SerializeField] Transform itemPosition;

    private void FixedUpdate()
    {

        if(roomInfo != null)
        {
            if(roomInfo.playerInRoom)
            {
                SpawnItem();
                Destroy(this);
            }
        }
    }

    void SpawnItem()
    {
        item = Instantiate(ItemManager.instance.itemTable.OpenGoldChest()) as GameObject;

        item.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        item.transform.SetParent(itemPosition);
        item.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void SetRoomInfo(GameObject obj)
    {
        roomInfoObject = obj;
        roomInfo = roomInfoObject.GetComponent<Room>();
    }
}
