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
    public List<AudioSource> sfxObjects;
    // ����Ʈ ����� �ش� ������Ʈ �����Ҷ� ���⼭ ���� �������� ������ݽô�.

    [Header("Sound State")]
    [SerializeField] private int[] volumes;
    // [0] master   [1] BGM   [2] SFX
    private void Start()
    {
        SoundInit();
        ObjectInit();
    }

    public int[] GetVolumes()
    {
        return volumes;
    }
    public int VolumeControl(int mode, int increase)
    {
        // mode 0 : master  1 : bgm  2: sfx
        switch(increase)
        {
            // ���� ����
            case 1:
                if (volumes[mode] < 9)
                {
                    volumes[mode]++;
                    ObjectVolumeControl(mode);
                }
                return volumes[mode];

            // ���� ����
            case 2:
                if (volumes[mode] > 0)
                {
                    volumes[mode]--;
                    ObjectVolumeControl(mode);
                }
                return volumes[mode];
        }
        return 0;
    }
    void ObjectVolumeControl(int mode)
    {
        switch(mode)
        {
            // master volume
            case 0:
                bgmObject.volume = (volumes[1] / 9.0f) * (volumes[0] / 9.0f);
                
                break;

            // bgm volume
            case 1:
                bgmObject.volume = (volumes[1] / 9.0f) * (volumes[0] / 9.0f);
                break;

            // sfx volume
            case 2:

                break;

        }
    }
    void ObjectInit()
    {
        bgmObject.volume = volumes[1] / 9.0f;
        // 1.������Ʈ ���� �ʱ�ȭ �־��ֱ�.
    }

    void SoundInit()
    {
        volumes = new int[3];
        volumes[0] = 9;
        volumes[1] = 3;
        volumes[2] = 5;

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
