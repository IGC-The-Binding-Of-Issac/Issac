using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ActiveInfo
{
    public Sprite redTearImg;

    private void Start()
    {
        //1�� ������, 6ĭ �ʿ���
        SetActiveItem(1, 6);
    }

    public override void UseActiveItem()
    {
        PlayerManager.instance.playerMaxHp += 1;
        PlayerManager.instance.playerMoveSpeed -= 0.18f;
        PlayerManager.instance.playerDamage += 2.0f;

        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = redTearImg;
    }
}
