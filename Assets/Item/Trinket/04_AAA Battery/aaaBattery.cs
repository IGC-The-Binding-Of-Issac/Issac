using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaBattery : TrinketInfo

{
    // Start is called before the first frame update
    private void Start()
    {
        SetTrinketItemCode(4);
    }

    public override void GetItem()
    {
        //���� �� ���͸� ���� ȿ���� 2��� �þ��. �����ϸ� �־��ְ� �ƴϸ� ����
        PlayerManager.instance.playerMoveSpeed += 0.1f;
    }
}
