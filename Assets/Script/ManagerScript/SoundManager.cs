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
    public AudioSource[] enemyObject; // ���� ���� ������Ʈ 
    public AudioSource[] stageObject; // �� ������Ʈ ���� ������Ʈ 
    // ����Ʈ ����� �ش� ������Ʈ �����Ҷ� ���⼭ ���� �������� ������ݽô�.

    [Header("Sound State")]
    [SerializeField] private float volumeBGM; // ��� ����
    [SerializeField] private float volumePlayer; // �÷��̾� ���� ����
    [SerializeField] private float volumeEffect; // �Ѿ� �׿� ����Ʈ ���
    [SerializeField] private float volumeObject; // �� ������Ʈ ����
    [SerializeField] private float volumeEnemy; // �� ����

    private void Start()
    {
        SoundInit();
        volumeBGM = bgmObject.volume; // ��� ���� ��ġ ��
    }

    void SoundInit()
    {
        volumeBGM = 0.5f;
        volumePlayer = 0.5f;
        volumeEffect = 0.5f;
        volumeObject = 0.5f;
        volumeEnemy = 0.5f;
    }

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

    public void PlayerHitSound()
    {
       
    }
    public void PlayerDeadSound()
    {

    }

    public void VolumeControl()
    {
        // ��� ���� ( SoundManager BGM ����ϴ� ������Ʈ ����)
        // ���

        // ������Ʈ ����  ( �ش� ������Ʈ���� �����ɶ� ������Ʈ���� �޾�����ҵ� )
        // ��, ��, �� ������ �Ҹ�
        // �� ������ �Ҹ� ( Ŭ���� ������ )
        // ���� ������ �Ҹ� 

        // �÷��̾� ���� ( GameManager �� playerobject�� �÷��̾ ����ϴ� ���� ������Ʈ ����.
        // ����� 
        // �ǰݽ�
        // ����/����/��ź �Դ� �Ҹ�
        // ������ ȹ��|��ü ����

        // ����Ʈ ����
        // ��ź ������ �Ҹ� ( ��ġ�ɶ� ���� �Ŵ������� ������ġ �����ͼ� �������ֱ� )
        // ���� ������ �Ҹ� 
        // ����/Ȳ�ݹ� �� ���� �Ҹ� ( ����ҋ� �����ͼ� �������ݽô�. )

        // ���� ���� ( �� �濡 enemis ���� Ȱ�� �� )
        // �⺻ ����
        // ��� ����
    }
}
