using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Image trinket; // ��ű�
    [SerializeField] Image active;  // ��Ƽ��
    [SerializeField] Text coinText; // ����
    [SerializeField] Text bombText; // ��ź
    [SerializeField] Text keyText;  // ����

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (ItemManager.instance.TrinketItem != null)
        {
            trinket.sprite = ItemManager.instance.TrinketItem.GetComponent<SpriteRenderer>().sprite;
        }
        coinText.text = ItemManager.instance.coinCount.ToString();
        bombText.text = ItemManager.instance.bombCount.ToString();
        keyText.text = ItemManager.instance.keyCount.ToString();
    }
}
