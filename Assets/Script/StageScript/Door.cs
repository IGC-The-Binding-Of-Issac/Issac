using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform movePosition;
    public GameObject roomInfo;
    public int doorDir = -1;

    [Header("Unity Setup")]
    public GameObject closeDoor;
    public GameObject openDoor;
    public bool doorKey;

    public void CheckedClear()
    {
        // door key : true :  usingKey / normal room / boss room
        if(roomInfo.GetComponent<Room>().isClear && doorKey)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void UsingKey()
    {
        doorKey = true;
    }

    public void OpenDoor()
    {
        closeDoor.SetActive(false);
        openDoor.SetActive(true);
    }
    public void CloseDoor()
    {   
        closeDoor.SetActive(true);
        openDoor.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(roomInfo.GetComponent<Room>().isClear) // ���� Ŭ����Ȼ��� �϶�
        {
            if(collision.gameObject.CompareTag("Player") && doorKey) // ���� �ε��� ����� �÷��̾���
            {
                collision.transform.position = movePosition.transform.position; // �÷��̾ �̵�
            }
        }
    }
}
