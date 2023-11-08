using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemonMishap : ActiveInfo
{
    [SerializeField]
    private GameObject oops;

    private void Awake()
    {
        SetActiveItem(18, 1);
        SetActiveString("����� ���",
                        "�� ...",
                        "��� �� ĳ���� �ֺ��� ��� ������ ���."
                      + "\n���ǿ� ��� ���� �ʴ� 24�� ���ظ� �Դ´�.");
    }

    public override void UseActive()
    {
        if (canUse)
        {
            Transform currentPosition = GameManager.instance.playerObject.GetComponent<PlayerController>().itemPosition;
            GameObject flooring = Instantiate(oops, new Vector3(currentPosition.position.x, currentPosition.position.y - 1f, 0), Quaternion.identity);
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
            
        }
    }
}
