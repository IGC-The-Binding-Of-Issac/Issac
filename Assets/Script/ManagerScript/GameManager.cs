using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Unity Setup")]
    public StageGenerate stageGenerate;
    public RoomGenerate roomGenerate;
    public GameObject myCamera;

    private void Start()
    {
        SetStage(1); // 1stage create
        roomGenerate.SetPrefabs(); // room Prefabs Setting
    }

    void Update()
    {
        // �������� ���� �׽�Ʈ.
        // RŰ ������ -> �������� ���۽÷� ���� �Ұ�.
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            if (playerObject != null)
                Destroy(playerObject);
            myCamera.transform.SetParent(null);
            StageStart();
        }
    }

    void StageStart()
    {
        // Create stage/room
        int cnt = 10;
        while (cnt-- > 0)
        {
            if (stageGenerate.CreateStage(stageSize, stageMinimunRoom))
            {
                roomGenerate.ClearRoom(); // room reset -> create
                roomGenerate.CreateRoom(stageLevel, stageSize); // room create
                break;
            }
        }
    }

    void SetStage(int stage)
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
