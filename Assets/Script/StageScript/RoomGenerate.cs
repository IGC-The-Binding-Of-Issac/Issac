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

    public List<GameObject> itemList; // ������ ������ ������Ʈ��

    [Header("Unity Setup")]
    public EnemyGenerate enemyGenerate;
    public RoomPattern pattern; // ������Ʈ ����
    public Transform roomPool; // �� �Ѱ��� ��Ƶ� ������Ʈ
    public GameObject[] rooms; // room prefabs�� ������������ ����� ������Ʈ
    public GameObject[] objectPrefabs; // ������� ������Ʈ �����Ҷ� ����� �����յ�
    public GameObject[] doorPrefabs;   // ������� �� �����Ҷ� ����� �����յ�

    [Header("Pooling")]
    public Transform rockPool_Transform;
    Stack<GameObject> rockPool = new Stack<GameObject>();

    public Transform poopPool_Transform;
    Stack<GameObject> poopPool = new Stack<GameObject>();

    public Transform firePool_Transform;
    Stack<GameObject> firePool = new Stack<GameObject>();

    public Transform spikePool_Transform;
    Stack<GameObject> spikePool = new Stack<GameObject>();

    public void SetObjectPooling()
    {
        rockPool = new Stack<GameObject>();
        poopPool = new Stack<GameObject>();
        firePool = new Stack<GameObject>();
        spikePool = new Stack<GameObject>();

        for(int i = 0; i < 40; i++)
        {
            // ������Ʈ ����
            GameObject rock = Instantiate(objectPrefabs[0], rockPool_Transform.position, Quaternion.identity);
            GameObject poop = Instantiate(objectPrefabs[1], poopPool_Transform.position, Quaternion.identity);
            GameObject fire = Instantiate(objectPrefabs[2], firePool_Transform.position, Quaternion.identity); 
            GameObject spike = Instantiate(objectPrefabs[3]);

            rockPool.Push(rock);
            poopPool.Push(poop);
            firePool.Push(fire);
            spikePool.Push(spike);

            // ������Ʈ �Ѱ��� ��Ƶα�.
            rock.transform.SetParent(rockPool_Transform);
            poop.transform.SetParent(poopPool_Transform);
            fire.transform.SetParent(firePool_Transform);
            spike.transform.SetParent(spikePool_Transform);

            // ���� ������ ���� SFXObject�� �־��ֱ�
            SetSFXObject(rock);
            SetSFXObject(poop);
            SetSFXObject(fire);
            SetSFXObject(spike);
        }
    }

    GameObject GetObstacle(int num)
    {
        switch(num)
        {
            #region ��
            case 0: // �� 
                if(rockPool.Count == 0) // ������ƮǮ�� ������Ʈ�� ������
                {
                    // ������Ʈ �����ؼ� ����
                    GameObject rock = Instantiate(objectPrefabs[0], rockPool_Transform.position, Quaternion.identity);
                    rockPool.Push(rock);
                    rock.transform.SetParent(rockPool_Transform);
                    SetSFXObject(rock);
                    return rockPool.Pop();
                }
                return rockPool.Pop();
            #endregion

            #region ��
            case 1: // ��
                if (poopPool.Count == 0) // ������ƮǮ�� ������Ʈ�� ������
                {
                    // ������Ʈ �����ؼ� ����
                    GameObject poop = Instantiate(objectPrefabs[1], poopPool_Transform.position, Quaternion.identity);
                    poopPool.Push(poop);
                    poop.transform.SetParent(poopPool_Transform);
                    SetSFXObject(poop);
                    return poopPool.Pop();
                }
                return poopPool.Pop();
            #endregion

            #region ��
            case 2: // ��
                if (firePool.Count == 0) // ������ƮǮ�� ������Ʈ�� ������
                {
                    // ������Ʈ �����ؼ� ����
                    GameObject fire = Instantiate(objectPrefabs[2], firePool_Transform.position, Quaternion.identity);
                    firePool.Push(fire);
                    fire.transform.SetParent(firePool_Transform);
                    SetSFXObject(fire);
                    return firePool.Pop();
                }
                return firePool.Pop();
            #endregion

            #region ����
            case 3: // ����
                if (spikePool.Count == 0) // ������ƮǮ�� ������Ʈ�� ������
                {
                    // ������Ʈ �����ؼ� ����
                    GameObject spike = Instantiate(objectPrefabs[3], spikePool_Transform.position, Quaternion.identity);
                    spikePool.Push(spike);
                    spike.transform.SetParent(spikePool_Transform);
                    SetSFXObject(spike);
                    return spikePool.Pop();
                }
                return spikePool.Pop();
            #endregion
        }
        return null;
    }
    void AllReturnObject(GameObject obj)
    {

        if (obj.GetComponent<Rock>() != null)
        {
            obj.GetComponent<Rock>().ResetObject();
            rockPool.Push(obj);
        }

        else if(obj.GetComponent<Poop>() != null)
        {
            obj.GetComponent<Poop>().ResetObject();
            poopPool.Push(obj);
        }

        else if(obj.GetComponent<FirePlace>() != null)
        {
            obj.GetComponent<FirePlace>().ResetObject();
            firePool.Push(obj);
        }

        else if(obj.GetComponent<Spikes>() != null)
        {
            obj.GetComponent<Spikes>().ResetObject();
            spikePool.Push(obj);
        }
    }

    public void SetPrefabs()
    {
        /* 
         * roomPrefabs[ �������� - 1 , ���ȣ - 1 ]
         * (-1 : ����X)  (0 : ���۹�)  (1 : �Ϲݹ�)  (2 : ������)  (3 : ������)  (4 : Ȳ�ݹ�) (5 : ���ֹ�)
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
        roomList = null; // ������ �� ������Ʈ �迭 �ʱ�ȭ
        doors = new List<GameObject>(); // ������ �� ������Ʈ �迭 �ʱ�ȭ

        // �����Ǿ��ִ� ��� ����� ����
        for(int i = 0; i < roomPool.childCount; i++)
        {
            Destroy(roomPool.GetChild(i).gameObject);
        }

        // ������ ������ ������Ʈ List �ʱ�ȭ
        if (itemList == null)
        {
            itemList = new List<GameObject>();
        }

        else 
        {
            // �����Ǿ��ִ� �������������� ���� ���� �� �ʱ�ȭ
            for(int i = 0; i < itemList.Count; i++)
            {
                Destroy(itemList[i]);
            }
            itemList = new List<GameObject>();
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
                int roomNum = GameManager.instance.stageGenerate.stageArr[i, j]; // �������� �������� �� ��ȣ

                // ����ִ¹�
                if (roomNum == 0)
                {
                    roomPos += new Vector3(15, 0, 0);
                    continue;
                }
                // �׿�

                // �� ������Ʈ ����
                GameObject room = Instantiate(roomPrefabs[stage - 1, roomNum - 1], roomPos, Quaternion.identity) as GameObject; // �� ������Ʈ�� ����.
                SetSFXObject(room); // �ش� ���� Sound Manager�� SFXObject�� �߰�
                roomList[i, j] = room; 
                roomList[i, j].GetComponent<Room>().isClear = false; // �ش� ���� ��Ŭ���� ���·� ��ȯ

                // (4 ����) (5 Ȳ��) (6 ����) (7 ����) �� ��� �ٷ� Ŭ���� ���·� ��ȯ
                if (roomNum == 4 || roomNum == 5 || roomNum == 6 || roomNum == 1)
                    roomList[i, j].GetComponent<Room>().isClear = true;
                

                // �� ������Ʈ ����
                roomList[i, j].GetComponent<Room>().SetGrid(); //
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

            // ���� ���� ������.
            if (doorNumder == 4)
                door.GetComponent<Door>().doorDamage = 1;
            else
                door.GetComponent<Door>().doorDamage = 0;
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
        for (int i = 0; i < rdPattern.GetLength(0); i++)
        {
            for(int j = 0; j < rdPattern.GetLength(1); j++)
            {
                int pNum = rdPattern[i, j]; ;
                if (pNum == 0)
                {
                    idx++;
                    continue;
                }

                if (pNum == 5) // ���� ������Ʈ�϶�
                {
                    // ������ �Ϲݸ��͸� ��ȯ����.
                    GameObject enemy = enemyGenerate.GetEnemy();
                    enemy.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]);
                    enemy.transform.localPosition = new Vector3(0, 0, 0);
                    enemy.GetComponent<Enemy>().roomInfo = roomList[y, x];
                    roomList[y, x].GetComponent<Room>().enemis.Add(enemy); // �ش� ���� ���͸���Ʈ�� �߰�

                    // sfx ���� ������ ���� ������Ʈ ����
                    SetSFXObject(enemy);
                }

                // �� ��
                else
                {
                    if (pNum == 10) // �÷��̾� ������Ʈ�϶�
                    {
                        Transform pos = roomList[y, x].GetComponent<Room>().roomObjects[idx].transform;
                        GameManager.instance.playerObject.transform.position = pos.position;
                    }

                    // �׿�
                    else
                    {
                        GameObject obstacle = Instantiate(objectPrefabs[pNum - 1]) as GameObject; // ������Ʈ ����
                        obstacle.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]); // ������Ʈ ��ġ ����
                        obstacle.transform.localPosition = new Vector3(0, 0, 0); // ������Ʈ ��ġ ����

                        if (pNum == 7) // Ȳ�ݹ� ������ ���̺�
                        {
                            obstacle.GetComponent<GoldTable>().SetRoomInfo(roomList[y,x]);
                        }
                        else if(pNum == 6) // ������ ������ ���̺�
                        {
                            obstacle.GetComponent<ShopTable>().SetRoomInfo(roomList[y, x]);
                        }

                        // sfx ���� ������ ���� ������Ʈ ����
                        SetSFXObject(obstacle);
                    }

                }
                idx++;
            }
        }    
    }

    void SetSFXObject(GameObject obj)
    {
        // sfx ���� ������ ���� ������Ʈ ����
        if (obj.GetComponent<AudioSource>() != null)
            SoundManager.instance.sfxObjects.Add(obj.GetComponent<AudioSource>());
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
