using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomsBottleOfPills : ActiveInfo
{
    private void Awake()
    {
        SetActiveItem(17, 4);
        SetActiveString("������ �ິ",
            "������ �˾� ������",
            "��� �� ������ �˾� �ϳ��� ����Ѵ�.");
    }

    public override void UseActive()
    {
        int randomNum = Random.Range(5, 16);
        //GameObject pill = Instantiate(ItemManager.instance.itemTable.GetComponent<ItemTable>().ActiveItems) 
    }
}
