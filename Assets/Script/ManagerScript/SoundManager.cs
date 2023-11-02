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

    [Header("Sounds")]
    [SerializeField] AudioClip[] stageBGM;
    [SerializeField] AudioClip[] stageIntroBGM;

    [Header("Sound Object")]
    [SerializeField] AudioSource bgmObject;

    public void OnStageIntroBGM()
    {
        // ����ǰ��ִ� bgm�� ������  bgm ����.
        if (bgmObject.isPlaying)
            bgmObject.Stop();

        // ���� bgm�� ���� ���������� bgm���� ����
        bgmObject.clip = stageBGM[GameManager.instance.stageLevel - 1];

        // intro bgm�� �ݺ���������
        bgmObject.loop = true;

        // intro bgm ����
        bgmObject.Play();
    }
}
