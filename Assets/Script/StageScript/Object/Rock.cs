using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // ��ź�� �ǰ��� DestoryRock()�� ȣ��.
    public void DestroyRock()
    {
        dropItem();
        Destroy(gameObject);
    }

    void dropItem()
    {
        // ItemManage���� ������ ���� ����غ��ô�.
        //����,ü�� �� ��������� Ȯ�����. ���� �ʿ�.
    }
}
