using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannon : ActiveInfo
{
    [Header("beforeStatement")]
    float beforeTearSize;

    public void Awake()
    {
        base.Start();
        SetActiveItem(2, 1);
        SetActiveString("���� ����",
                        "�����ؼ� �ٷ缼��.",
                        "��� �� ĳ���Ͱ� ���� ������ �Ӹ� ���� ���" 
                      + "\n�ش� �������� �Ŵ��� ������ �߻��Ѵ�.");
        beforeTearSize = PlayerManager.instance.playerTearSize;
        Invoke("SetCanChangeItem", 1f);
    }

    public override void UseActive()
    {
        if (canUse)
        {
            StartCoroutine(ShootCannon());
            canUse = false;
            Invoke("SetCanUse", 1f);
            GameManager.instance.playerObject.GetComponent<PlayerController>().canChangeItem = false;
            Invoke("SetCanChangeItem", 1f);
        }
    }

    //������ ��� ������ ���� ������� �ǵ����ֱ�
    public override void afterActiveAttack()
    {
        PlayerManager.instance.playerTearSize = beforeTearSize;
        PlayerManager.instance.ChgTearSize();
    }

    private IEnumerator ShootCannon()
    {
        for (int l = 0; l < 2; l++)
        {
            float shootHor = Input.GetAxis("Horizontal");
            float shootVer = Input.GetAxis("Vertical");

            //Dr.Fetus �Ծ��� �� ��ź �߻� ������
            if (ItemManager.instance.PassiveItems[16] && l == 0) PlayerManager.instance.playerTearSize *= 2.3f;
            else if (l == 0)
            {
                PlayerManager.instance.playerTearSize *= 8f;
            }
            PlayerManager.instance.ChgTearSize();

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
            yield return new WaitForSeconds(0.5f);
        }
        afterActiveAttack();
        yield return null;
    } 

    
}

