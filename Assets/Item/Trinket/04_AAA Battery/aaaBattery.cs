using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaBattery : TrinketInfo

{
    float beforeDropMoveSpeed;
    private void Start()
    {
        SetTrinketItemCode(4);
        SetTrinketString("AAA ������",
            "���� ȿ�� ����",
            "���� �� �� Ŭ���� �� ��Ƽ�� ������ ���� ȿ���� 2�谡 �Ǹ�" +
            "�̵��ӵ� + 0.1");
    }

    public override void GetItem()
    {
        //���� �� ���͸� ���� ȿ���� 2��� �þ��. �����ϸ� �־��ְ� �ƴϸ� ����
        PlayerManager.instance.playerMoveSpeed += 0.1f;
    }

    public override void DropTrinket()
    {
        beforeDropMoveSpeed = PlayerManager.instance.playerMoveSpeed;
        PlayerManager.instance.playerMoveSpeed = beforeDropMoveSpeed - 0.1f;
    }
}
