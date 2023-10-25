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

    [Header("Hearts")]
    [SerializeField] Transform heartUI; // ��Ʈ UI 
    [SerializeField] GameObject emptyHeart; // ����Ʈ

    private void Start()
    {
        SetPlayerMaxHP(); // �ִ�ü�� �÷����ϴ°����� ȣ�����ֱ�.
        SetPlayerCurrentHP(); // ���� ü���� �ٲ������ ȣ�����ֱ� ( ex) ������ �Ծ����� )
    }

    private void Update()
    {
        UpdateUI();
        SetPlayerCurrentHP();
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

    public void SetPlayerMaxHP()
    {
        for (int i = 0; i < PlayerManager.instance.playerMaxHp/2; i++)
        {
            GameObject eheart = Instantiate(emptyHeart) as GameObject;
            eheart.transform.SetParent(heartUI);
        }
    }

    public void SetPlayerCurrentHP()
    {
        int tmp = PlayerManager.instance.playerHp;
        for(int i = 0; i < heartUI.childCount; i++)
        {
            if(tmp >= 2)
            {
                heartUI.GetChild(i).GetComponent<UIHeart>().SetHeart(2);
                tmp -= 2;
            }
            else if(tmp >= 1)
            {
                heartUI.GetChild(i).GetComponent<UIHeart>().SetHeart(1);
                tmp -= 1;
            }
            else
            {
                heartUI.GetChild(i).GetComponent<UIHeart>().SetHeart(0);
            }
        }
    }
}
