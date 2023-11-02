using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TammyHead : ActiveInfo
{
    float beforeDamage;
    private GameObject activeTear;
    void Awake()
    {
        SetActiveItem(3, 1);
        SetActiveString("�¹��� �Ӹ�",
            "������ ���� ����",
            "��� �� 8�������� �������� 25 �� ���� ������ ���ÿ� �߻��Ѵ�.");
        beforeDamage = PlayerManager.instance.playerDamage;
    }

    public override void UseActive()
    {
        if(canUse)
        {
        PlayerManager.instance.playerDamage += 25f;
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 0);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, -1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, 1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, -1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 1);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 0);
        GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, -1);
        activeTear = GameObject.Find("Tear(Clone)");
            canUse = false;
            Invoke("SetCanUse", 1f);
        }
    }

    public override void CheckedItem()
    {
        if (activeTear == null) PlayerManager.instance.playerDamage = beforeDamage;
    }
}
