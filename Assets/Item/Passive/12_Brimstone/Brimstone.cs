using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brimstone : ItemInfo
{

    private void Start()
    {
        SetItemCode(12);
        SetItemString("��Ȳ",
            "������",
            "����� ���ݼӵ� / 3��");
    }
    public override void UseItem()
    {
        PlayerManager.instance.playerShotDelay /= 0.33f;
        PlayerManager.instance.CheckedShotDelay();
        //���� �� ĳ���� �ܸ� ����
        //������ ��¡���� �ٲ�
        //Ű�� 3���̻� ������ ������ �����ϰ� Ű�� ���� �� �Կ��� �ش� �������� �߻�
        //Ű�� ������ ������ ������ �� ����
    }
}
