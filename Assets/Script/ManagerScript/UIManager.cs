using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region �̱���
    public static UIManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Text")]
    [SerializeField] Image trinket; // ��ű�
    [SerializeField] Image active;  // ��Ƽ��
    [SerializeField] Text coinText; // ����
    [SerializeField] Text bombText; // ��ź
    [SerializeField] Text keyText;  // ����

    [Header("Hearts")]
    [SerializeField] Transform heartUI; // ��Ʈ UI 
    [SerializeField] GameObject emptyHeart; // ����Ʈ

    [Header("Active")]
    [SerializeField] Image activeEnergy;
    [SerializeField] Sprite nullImage;

    [Header("UI")]
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject loadingImage;
    private void Start()
    {
        SetPlayerMaxHP(); // ��ƮHP �ʱ⼼��
        OnLoading();
    }

    private void Update()
    {
        AddHeart(); // �ִ�ü�� �����ϴ°����� ȣ�����ּ���
        DelHeart(); // �ִ�ü�� �����ϴ°����� ȣ�����ּ���
        SetPlayerCurrentHP(); // ���� ü���� ���� �Ǵ� �����Ҷ� ȣ�����ּ��� ( GetDamage / GetHeart  ��� )
        UpdateActiveEnergy(); // ��Ƽ�� ������ ������ �����Ǵ� ����(���)�Ҷ� ȣ�����ּ���.

        PauseUIOnOff(); // Pause UI
        UpdateUI(); // ���� UI ������Ʈ
    }

    void OnLoading()
    {
        loadingImage.SetActive(true);
        StartCoroutine(FadeOutStart());
    }
    public IEnumerator FadeOutStart()
    {
        yield return new WaitForSeconds(0.2f);
        for (float f = 1f; f > 0; f -= 0.005f)
        {
            Color c = loadingImage.GetComponent<Image>().color;
            c.a = f;
            loadingImage.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        loadingImage.SetActive(false);
    }

    #region PauseUI
    public void PauseUIOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseUI.activeSelf)
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            }

            else if(pauseUI.activeSelf)
            {
                pauseUI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void PauseExitBtn()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("01_Intro");
    }

    public void PauseResumeBtn()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    #endregion

    #region GameOverUI
    public void GameOver()
    {
        gameoverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameOverRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("02_Game");
    }
    #endregion

    #region HP
    public void SetPlayerMaxHP()
    {
        for (int i = 0; i < PlayerManager.instance.playerMaxHp/2; i++)
        {
            GameObject eheart = Instantiate(emptyHeart) as GameObject;
            eheart.transform.SetParent(heartUI);
        }
    }

    public void AddHeart() // �ִ�ü�� ����
    {
        int addCount = (PlayerManager.instance.playerMaxHp / 2) - heartUI.childCount;
        for (int i = 0; i < addCount; i++)
        {
            GameObject eheart = Instantiate(emptyHeart) as GameObject;
            eheart.transform.SetParent(heartUI);
        }
    }
    public void DelHeart() // �ִ� ü�� ����
    {
        int delCount = -((PlayerManager.instance.playerMaxHp / 2) - heartUI.childCount);
        for (int i = 0; i < delCount; i++)
        {
            Destroy(heartUI.GetChild(0).gameObject);
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
    #endregion

    public void UpdateActiveEnergy()
    {
        if(ItemManager.instance.ActiveItem != null)
        {
            ActiveInfo active = ItemManager.instance.ActiveItem.GetComponent<ActiveInfo>();
            if(active.needEnergy == 0)
            {
                activeEnergy.fillAmount = 1;
            }
            else
            {
                activeEnergy.fillAmount = active.currentEnergy / active.needEnergy;
            }
        }
    }

    public void UpdateUI()
    {
        if (ItemManager.instance.TrinketItem != null)
        {
            trinket.sprite = ItemManager.instance.TrinketItem.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            trinket.sprite = nullImage;
        }

        if (ItemManager.instance.ActiveItem != null)
        {
            active.sprite = ItemManager.instance.ActiveItem.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            active.sprite = nullImage;
        }


        coinText.text = ItemManager.instance.coinCount.ToString();
        bombText.text = ItemManager.instance.bombCount.ToString();
        keyText.text = ItemManager.instance.keyCount.ToString();
    }
}
