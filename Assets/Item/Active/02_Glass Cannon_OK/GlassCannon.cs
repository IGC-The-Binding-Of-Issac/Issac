using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannon : ActiveInfo
{
    float beforeTearSize;
    float beforeDamage;
    private GameObject tear;
    void Awake()
    {
        SetActiveItem(2, 1);
        SetActiveString("���� ����",
                        "�����ؼ� �ٷ缼��.",
                        "��� �� ĳ���Ͱ� ���� ������ �Ӹ� ���� ���" +
                        "\n�ش� �������� ��û���� �Ŵ��� ������ �߻��Ѵ�." +
                        "\n���ݷ��� ���� ���ݷ� * 10�̴�.");
        beforeTearSize = PlayerManager.instance.playerTearSize;
        beforeDamage = PlayerManager.instance.playerDamage;
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if (canUse)
        {
            float shootHor = Input.GetAxis("Horizontal");
            float shootVer = Input.GetAxis("Vertical");
            PlayerManager.instance.playerTearSize *= 8f;
            PlayerManager.instance.ChgTearSize();
            PlayerManager.instance.playerDamage *= 10f;
            if (shootHor == 0 && shootVer != 0)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(0, shootVer);
            }
            else if (shootHor != 0 && shootVer == 0)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(shootHor, 0);
            }
            else if (shootHor == 0 && shootVer == 0)
            {
                GameManager.instance.playerObject.GetComponent<PlayerController>().Shoot(1, 0);
            }
            tear = GameObject.Find("Tear(Clone)");
            afterActiveAttack();
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }

    public override void afterActiveAttack()
    {
        PlayerManager.instance.playerTearSize = beforeTearSize;
        PlayerManager.instance.ChgTearSize();
    }

    public override void CheckedItem()
    {
        if (tear == null)
        {
            PlayerManager.instance.playerDamage = beforeDamage;
        }
    }
}

