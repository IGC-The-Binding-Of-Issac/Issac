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
        if (canUse)
        {
            //��Ƽ�� ������ �� 5 ~ 15�������� �˾�. 
            int randomNum = Random.Range(5, 16);
            Transform dropPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
            //�� �߿� ������ ������ ������ �˾� ���� �� ����
            GameObject pill = Instantiate(ItemManager.instance.itemTable.ActiveChange(randomNum),
                                          new Vector3(dropPosition.position.x, dropPosition.position.y - 1f, 0),
                                          Quaternion.identity) as GameObject;
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }
}
