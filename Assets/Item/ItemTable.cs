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

    [SerializeField]
    GameObject[] TrinketItems; // ��ű� ������

    public GameObject ObjectBreak() // ������Ʈ �ν�����
    {
        int rd = Random.Range(0, DropItems.Length-1);
        return DropItems[rd]; // ���� ������ ��ȯ ( ���� ���� )
    }

    public GameObject OpenNormalChest(int rd)
    {
        return DropItems[rd];
    }


    public GameObject OpenGoldChest()
    {
        // ���Ȯ�� : ��ű� 60% / �нú� 20% / ��Ƽ�� 20%
        // �нú� : �̹� �������� ������ �ߺ���� X
        // ��Ƽ��/��ű� : �ѹ��̶� ������������ ���X
        // ��ű� �������� ���� ��������, �нú������ ���
        // �нú� �������� ���� ��������, ��Ƽ������� ���
        // ��Ƽ�� �������� ���� ��������, ������  

        // ��ű� -> �нú� -> ��Ƽ�� -> ����
        return PassiveItems[1];
    }



    public GameObject TrinketChange(int itemCode)
    {
        return TrinketItems[itemCode];
    }
}