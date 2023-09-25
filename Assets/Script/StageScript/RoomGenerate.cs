using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate : MonoBehaviour
{
    int[] dy = new int[4] { -1, 0, 1, 0 };
    int[] dx = new int[4] { 0, 1, 0, -1 };

    List<GameObject> doors; // ������ �� ������Ʈ��

    public GameObject[,] roomList; // ������ �� ������Ʈ��
    
    public GameObject[,] roomPrefabs; // ������� ����� ������

    [Header("Unity Setup")]
    public RoomPattern pattern; // ������Ʈ ����
    public Transform roomPool; // �� �Ѱ��� ��Ƶ� ������Ʈ
    public GameObject[] rooms; // room prefabs�� ������������ ����� ������Ʈ
    public GameObject[] objectPrefabs; // ������� ������Ʈ �����Ҷ� ����� �����յ�
    public GameObject[] doorPrefabs;   // ������� �� �����Ҷ� ����� �����յ�

    public void SetPrefabs()
    {
        /* 
         * roomPrefabs[ �������� - 1 , ���ȣ - 1 ]
         * (-1 : ����X)  (0 : ���۹�)  (1 : �Ϲݹ�)  (2 : ������)  (3 : ������)  (4 : Ȳ�ݹ�) (5 : ���ֹ�)
         * 
         * objects[]
         * (0 : ��)  (1 : ��)  (2 : ��ں�)  (3 : ������)
         * 
         * doorPrefabs[]
         * (0 : �Ϲݹ�)  (1 : ������)  (2 : ������)  (3 : ����)  (4 : ���ֹ�)
         */
        int cnt = 0;
        roomPrefabs = new GameObject[4, 6];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                roomPrefabs[i, j] = rooms[cnt++];
            }
        }
    }
    public void ClearRoom()
    {
        roomList = null;
        doors = new List<GameObject>();
        for(int i = 0; i < roomPool.childCount; i++)
        {
            Destroy(roomPool.GetChild(i).gameObject);
        }
    }
    public void CreateRoom(int stage, int size)
    {
        roomList = new GameObject[size, size];

        Vector3 roomPos = new Vector3(0, 0, 0);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int roomNum = GameManager.instance.stageGenerate.stageArr[i, j];
                if (roomNum == 0)
                {
                    roomPos += new Vector3(15, 0, 0);
                    continue;
                }
                // create room
                GameObject room = Instantiate(roomPrefabs[stage - 1, roomNum - 1], roomPos, Quaternion.identity) as GameObject;
                roomList[i, j] = room;
                roomList[i, j].GetComponent<Room>().isClear = false;

                if (roomNum == 4 || roomNum == 5 || roomNum == 6 || roomNum == 1)
                    roomList[i, j].GetComponent<Room>().isClear = true;
                

                // create obstacle
                roomList[i, j].GetComponent<Room>().SetGrid();
                CreateObstacle(i, j, roomNum);
                room.transform.SetParent(roomPool);

                roomPos += new Vector3(15, 0, 0);
            }
            roomPos = new Vector3(0, roomPos.y, 0);
            roomPos += new Vector3(0, -10, 0);
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int roomNum = GameManager.instance.stageGenerate.stageArr[i, j];
                if (roomNum == 0)
                {
                    continue;
                }
                CreateDoor(i, j);
            }
        }
    }
    public void CreateDoor(int y, int x)
    {
        for (int i = 0; i < 4; i++)
        {
            int ny = y + dy[i];
            int nx = x + dx[i];

            // out of range
            if (ny < 0 || nx < 0 || ny >= GameManager.instance.stageSize || nx >= GameManager.instance.stageSize)
                continue;

            // nextRoom  Null 
            if (GameManager.instance.stageGenerate.stageArr[ny, nx] == 0)
                continue;

            // �� ����
            int doorNumder = ChoiceDoor(y,x,ny,nx);

            GameObject door = Instantiate(doorPrefabs[doorNumder]) as GameObject;
            door.transform.SetParent(roomList[y, x].GetComponent<Room>().doorPosition[i]);
            door.transform.localPosition = new Vector3(0, 0, 0);
            door.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            door.GetComponent<Door>().doorDir = i; // door dir

            door.GetComponent<Door>().roomInfo = roomList[y, x]; // door in Room Info
            door.GetComponent<Door>().movePosition = roomList[ny, nx].GetComponent<Room>().movePosition[i]; // door MovePosition 

            int roomNum = GameManager.instance.stageGenerate.stageArr[y, x];
            if (roomNum == 4 || roomNum == 5 || roomNum == 6)
                door.GetComponent<Door>().UsingKey();

            doors.Add(door);
        }
    }
    int ChoiceDoor(int y, int x, int ny, int nx)
    {
        int doorNum;
        int roomNum = GameManager.instance.stageGenerate.stageArr[y, x];
        int nextRoomNum = GameManager.instance.stageGenerate.stageArr[ny, nx];
        // ������� ����,����,Ȳ��,���� �϶�
        if (3 <= roomNum && roomNum <= 6)
        {
            doorNum = roomNum - 2;
        }
        // ������� ����, �Ϲ� �϶�
        else
        {
            // �������� ����,����,Ȳ��,���� �϶�
            if (3 <= nextRoomNum && nextRoomNum <= 6)
            {
                doorNum = nextRoomNum - 2;
            }
            else
            {
                doorNum = 0;
            }
        }

        return doorNum;
    }

    void CreateObstacle(int y, int x, int roomNumber)
    {
        int idx = 0;
        int[,] rdPattern = pattern.GetPattern(roomNumber); 
        for(int i = 0; i < rdPattern.GetLength(0); i++)
        {
            for(int j = 0; j < rdPattern.GetLength(1); j++)
            {
                if (rdPattern[i, j] == 0)
                {
                    idx++;
                    continue;
                }
                GameObject obstacle = Instantiate(objectPrefabs[rdPattern[i, j] - 1]) as GameObject;
                obstacle.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]);
                obstacle.transform.localPosition = new Vector3(0, 0, 0);
                idx++;

                if (rdPattern[i, j] == 10) // �÷��̾� ������Ʈ�϶�
                {
                    obstacle.transform.SetParent(null);
                    GameManager.instance.playerObject = obstacle;
                }
            }
        }    
    }


    private void Update()
    {
        if(doors != null)
        {
            for(int i = 0; i < doors.Count; i++)
            {
                doors[i].GetComponent<Door>().CheckedClear();
            }
        }
    }

}
