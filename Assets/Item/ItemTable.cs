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

    [SerializeField]
    GameObject[] ActiveItems; // ��Ƽ�� ������

    [SerializeField] private List<int> passive;
    [SerializeField] private List<int> trinket;
    [SerializeField] private List<int> active;
    private void Start()
    {
        // �ߺ� ��� ������  �ʱ�ȭ
        passive = new List<int>();
        trinket = new List<int>();
        active = new List<int>();
        for(int i = 0; i < PassiveItems.Length; i++)
            passive.Add(i);

        for (int i = 0; i < TrinketItems.Length; i++)
            trinket.Add(i);

        for (int i = 0; i < ActiveItems.Length; i++)
            active.Add(i);
    }
    public GameObject ObjectBreak() // ������Ʈ �ν�����
    {
        int rd = Random.Range(0, DropItems.Length-1);
        return DropItems[rd]; // ���� ������ ��ȯ ( ���� ���� )
    }

    public GameObject OpenNormalChest(int rd)
    {
        return DropItems[rd];
    }

    public GameObject DropTrinket()
    {
        GameObject obj;
        if (trinket.Count == 0)
        {
            if(passive.Count == 0)
            {
                if(active.Count == 0)
                {
                    obj = DropItems[3];
                    return obj;
                }
                ShuffleList(ref active);
                obj = ActiveItems[active[0]];
                active.RemoveAt(0);
                return obj;
            }

            ShuffleList(ref passive);
            obj = PassiveItems[passive[0]];
            passive.RemoveAt(0);
            return obj;
        }

        ShuffleList(ref trinket);
        obj = TrinketItems[trinket[0]];
        trinket.RemoveAt(0);
        return obj;
    }

    public GameObject DropPassive()
    {
        GameObject obj;
        if (passive.Count == 0)
        {
            if (active.Count == 0)
            {
                if (trinket.Count == 0)
                {
                    obj = DropItems[3];
                    return obj;
                }
                ShuffleList(ref trinket);
                obj = TrinketItems[trinket[0]];
                trinket.RemoveAt(0);
                return obj;
            }

            ShuffleList(ref active);
            obj = ActiveItems[active[0]];
            active.RemoveAt(0);
            return obj;
        }

        ShuffleList(ref passive);
        obj = PassiveItems[passive[0]];
        passive.RemoveAt(0);
        return obj;
    }

    public GameObject DropActive()
    {
        GameObject obj;
        if (active.Count == 0)
        {
            if (passive.Count == 0)
            {
                if (trinket.Count == 0)
                {
                    obj = DropItems[3];
                    return obj;
                }
                ShuffleList(ref trinket);
                obj = TrinketItems[trinket[0]];
                trinket.RemoveAt(0);
                return obj;
            }
            ShuffleList(ref passive);
            obj = PassiveItems[passive[0]];
            passive.RemoveAt(0);
            return obj;
        }
        ShuffleList(ref active);
        obj = ActiveItems[active[0]];
        active.RemoveAt(0);
        return obj;
    }

    public GameObject OpenGoldChest()
    {
        int rd = Random.Range(0, 10000);
        int n = rd % 100;

        if (0 <= n && n <= 49)
            return DropTrinket();

        else if (50 <= n && n <= 75)
            return DropPassive();

        else
            return DropActive();
    }

    public GameObject TrinketChange(int itemCode)
    {
        return TrinketItems[itemCode];
    }

    public GameObject ActiveChange(int itemCode)
    {
        return ActiveItems[itemCode];
    }
    private void ShuffleList(ref List<int> list)
    {
        int rd1, rd2;
        int tmp;

        for(int i = 0; i < list.Count; i++)
        {
            rd1 = Random.Range(0, list.Count);
            rd2 = Random.Range(0, list.Count);

            tmp = list[rd1];
            list[rd1] = list[rd2];
            list[rd2] = tmp;
        }
    }
   
}