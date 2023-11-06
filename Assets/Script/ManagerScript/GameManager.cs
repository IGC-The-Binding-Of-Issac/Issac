using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region singleTon
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    public int stageLevel; // ���� �������� ����
    public int stageSize;  // �������� ũ��
    public int stageMinimunRoom; // �������� �� �ּҰ���
    public GameObject playerObject; // ������ �÷��̾�
    public int maxStage;

    [Header("Unity Setup")]
    public StageGenerate stageGenerate;
    public RoomGenerate roomGenerate;
    public GameObject myCamera;

    private void Start()
    {
        SetStage(1); // 1stage create
        roomGenerate.SetPrefabs(); // room Prefabs Setting

        // ���� ���� ( �������� ���� )
        Invoke("StageStart", 0.3f);
    }

    void Update()
    {
        // �������� ���� �׽�Ʈ.
        // RŰ ������ -> �������� ���۽÷� ���� �Ұ�. 
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            UIManager.instance.OnLoading();
            StageStart();
            
        }
    }
    public void StageStart()
    {
        // Create stage/room

        if (playerObject == null)
        {
            GameObject obj = Instantiate(roomGenerate.objectPrefabs[9]) as GameObject;
            playerObject = obj;
        }

        myCamera.transform.SetParent(null);

        int cnt = 15;
        while (cnt-- > 0)
        {
            if (stageGenerate.CreateStage(stageSize, stageMinimunRoom))
            {
                roomGenerate.ClearRoom(); // room reset -> create
                roomGenerate.CreateRoom(stageLevel, stageSize); // room create
                myCamera.transform.position = playerObject.transform.position;

                SoundManager.instance.OnStageBGM();
                break;
            }
        }
    }

    public void NextStage()
    {
        SetStage(++stageLevel); // �������� ����
        UIManager.instance.OnLoading();
        StageStart(); // �������� ����
    }

    public void SetStage(int stage)
    {
        stageLevel = stage;
        switch(stageLevel)
        {
            case 1: // 1��������
                stageSize = 5;
                stageMinimunRoom = 8;
                break;
            case 2: // 2��������
                stageSize = 5;
                stageMinimunRoom = 8;
                break;
            case 3: // 3��������
                stageSize = 7;
                stageMinimunRoom = 10;
                break;
            case 4: // 4��������
                stageSize = 7;
                stageMinimunRoom = 12;
                break;
            default:
                stageSize = 5;
                stageMinimunRoom = 8;
                break;
        }
    }
}
