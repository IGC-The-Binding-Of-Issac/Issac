using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Image trinket; // ��ű�
    [SerializeField] Image active;  // ��Ƽ��
    [SerializeField] Text CoinText; // ����
    [SerializeField] Text BombText; // ��ź
    [SerializeField] Text KeyText;  // ����

    private void Update()
    {
        if(ItemManager.instance.TrinketItem != null)
        {
            trinket.sprite = ItemManager.instance.TrinketItem.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
