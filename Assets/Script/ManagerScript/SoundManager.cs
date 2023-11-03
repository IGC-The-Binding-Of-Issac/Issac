using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region singleton
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    [Header("Audio Cilps")]
    [SerializeField] AudioClip[] stageBGM;
    [SerializeField] AudioClip[] stageIntroBGM;

    [Header("Sound Object")]
    public AudioSource bgmObject; // BGM ���� ������Ʈ
    public AudioSource playerObject;  // �÷��̾� ���� ������Ʈ
    public List<AudioSource> enemyObject; // ���� ���� ������Ʈ 
    public List<AudioSource> stageObject; // �� ������Ʈ ���� ������Ʈ 
    // ����Ʈ ����� �ش� ������Ʈ �����Ҷ� ���⼭ ���� �������� ������ݽô�.

    [Header("Sound State")]
    [SerializeField] private float volumeMaster; // ����
    [SerializeField] private float volumeBGM; // ����
    [SerializeField] private float volumeSFX; // ����

    private void Start()
    {
        SoundInit();

        bgmObject.volume = volumeBGM;
    }

    void SoundInit()
    {
        volumeMaster = 1.0f;
        volumeBGM = 0.5f;
        volumeSFX = 0.5f;

        enemyObject = new List<AudioSource>();
        stageObject = new List<AudioSource>();
    }

    public void OnStageBGM()
    {
        // ����ǰ��ִ� bgm�� ������  bgm ����.
        if (bgmObject.isPlaying)
            bgmObject.Stop();

        // ���� bgm�� ���� ���������� bgm���� ����
        bgmObject.clip = stageBGM[GameManager.instance.stageLevel - 1];

        // bgm �ݺ�
        bgmObject.loop = true;

        // bgm ����
        bgmObject.Play();
    }
    /*
Master Volume 
  -
Music Volume 
  - BGM
SFX Volume
  - �÷��̾� �ǰ� ����
  - �÷��̾� ��� ����
  - ����/����/��ź ȹ�� ����
  - ������ ȹ��/��ü ����
  - �÷��̾� ���� ������ �Ҹ�
  - ��ź ������ �Ҹ�
  - ��,��,�� ������ ����
  - �� Ŭ����� �������� ����
  - ���� ���½� ����
  - ����/Ȳ�ݹ� �� ���� �Ҹ�
  - ���� �⺻ ����
  - ���� ��� ����
*/
}
