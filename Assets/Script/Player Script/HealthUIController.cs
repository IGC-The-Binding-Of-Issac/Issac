using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{

    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite redHeart;
    public Sprite halfRedHeart;

    public Sprite soulHeart;
    public Sprite halfSoulHeart;

    public Sprite blackHeart;
    public Sprite halfBlackHeart;

    public Sprite eternalHeart;

    void Update()
    {
        HpSystem();
    }

    void HpSystem()
    {
        //���� ü��
        int hp = GameController.instance.playerHp;
        //�ִ� ü��
        int maxHp = GameController.instance.playerMaxHp;
        for (int i = 0; i < hearts.Length; i++)
        {
            //���� ü���� �ִ� ü���� ���� �ʰ� ��
            if (hp > maxHp)
            {
                hp = maxHp;
            }
            //���� ü�¸�ŭ ü�� sprite Ȱ��ȭ
            if (i < hp)
            {
                if (i % 2 == 1)
                {
                    hearts[i].sprite = redHeart;
                }
                else
                {
                    hearts[hp-1].sprite = halfRedHeart;
                }
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            //�ִ� ü�¸�ŭ �� ü�� Ȱ��ȭ
            if (i < maxHp)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    
}