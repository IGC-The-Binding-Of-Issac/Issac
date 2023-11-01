using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNail : ActiveInfo
{
    public Sprite redTearImg;

    private void Awake()
    {
        //0�� ������, 6ĭ �ʿ���
        SetActiveItem(0, 6);
    }

    public override void UseActive()
    {
        PlayerManager.instance.playerMaxHp += 1;
        PlayerManager.instance.playerMoveSpeed -= 0.08f;
        PlayerManager.instance.playerDamage += 0.5f;

        PlayerManager.instance.tearObj.GetComponent<SpriteRenderer>().sprite = redTearImg;
    }
    public override void CheckedItem()
    {
       
    }
}
