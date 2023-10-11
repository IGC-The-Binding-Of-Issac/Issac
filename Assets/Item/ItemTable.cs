using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    [SerializeField]
    GameObject[] DropItems; // ���������
    // 0 : ���� , 1 : ��Ʈ , 2 : ��ź , 3 : ����

    [SerializeField]
    GameObject[] PassiveItems; // �нú� ������



    public GameObject ObjectBreak() // ������Ʈ �ν�����
    {
        int rd = Random.Range(0, DropItems.Length-1);
        return DropItems[rd]; // ���� ������ ��ȯ ( ���� ���� )
    }

    public GameObject OpenNormalChest(int rd)
    {
        return DropItems[rd];
    }
}
