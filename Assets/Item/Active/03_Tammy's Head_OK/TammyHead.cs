using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TammyHead : ActiveInfo
{
    private int shootCount;
    void Awake()
    {
        SetActiveItem(3, 1);
        SetActiveString("�¹��� �Ӹ�",
                        "������ ���� ����",
                        "��� �� 8�������� ������ ���ÿ� �߻��Ѵ�.");
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if(canUse)
        {
            StartCoroutine(TammyHeadAttack());
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }
    private IEnumerator TammyHeadAttack()
    {
        for(int i = 0; i < 2; i++) 
        { 
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 0);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, -1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, -1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 1);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, 0);
            GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(-1, -1);
            yield return new WaitForSeconds(0.25f);
        }
    }

}
