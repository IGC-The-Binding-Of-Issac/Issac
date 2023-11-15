using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate : MonoBehaviour
{
    int[] dy = new int[4] { -1, 0, 1, 0 };
    int[] dx = new int[4] { 0, 1, 0, -1 };

    List<GameObject> doors; // 생성된 문 오브젝트들

    public GameObject[,] roomList; // 생성된 방 오브젝트들
    
    public GameObject[,] roomPrefabs; // 방생성에 사용할 프래핍

    public List<GameObject> itemList; // 생성된 아이템 오브젝트들

    [Header("Unity Setup")]
    public EnemyGenerate enemyGenerate;
    public RoomPattern pattern; // 오브젝트 패턴
    public Transform roomPool; // 방 한곳에 모아둘 오브젝트
    public GameObject[] rooms; // room prefabs에 스테이지별로 담아줄 오브젝트
    public GameObject[] objectPrefabs; // 방생성후 오브젝트 생성할때 사용할 프리팹들
    public GameObject[] doorPrefabs;   // 방생성후 문 생성할때 사용할 프리팹들

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
            // 오브젝트 생성
            GameObject rock = Instantiate(objectPrefabs[0], rockPool_Transform.position, Quaternion.identity);
            GameObject poop = Instantiate(objectPrefabs[1], poopPool_Transform.position, Quaternion.identity);
            GameObject fire = Instantiate(objectPrefabs[2], firePool_Transform.position, Quaternion.identity); 
            GameObject spike = Instantiate(objectPrefabs[3]);

            rockPool.Push(rock);
            poopPool.Push(poop);
            firePool.Push(fire);
            spikePool.Push(spike);

            // 오브젝트 한곳에 모아두기.
            rock.transform.SetParent(rockPool_Transform);
            poop.transform.SetParent(poopPool_Transform);
            fire.transform.SetParent(firePool_Transform);
            spike.transform.SetParent(spikePool_Transform);

            // 사운드 조절을 위해 SFXObject로 넣어주기
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
            #region 돌
            case 0: // 돌 
                if(rockPool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
                    GameObject rock = Instantiate(objectPrefabs[0], rockPool_Transform.position, Quaternion.identity);
                    rockPool.Push(rock);
                    rock.transform.SetParent(rockPool_Transform);
                    SetSFXObject(rock);
                    return rockPool.Pop();
                }
                return rockPool.Pop();
            #endregion

            #region 똥
            case 1: // 똥
                if (poopPool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
                    GameObject poop = Instantiate(objectPrefabs[1], poopPool_Transform.position, Quaternion.identity);
                    poopPool.Push(poop);
                    poop.transform.SetParent(poopPool_Transform);
                    SetSFXObject(poop);
                    return poopPool.Pop();
                }
                return poopPool.Pop();
            #endregion

            #region 불
            case 2: // 불
                if (firePool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
                    GameObject fire = Instantiate(objectPrefabs[2], firePool_Transform.position, Quaternion.identity);
                    firePool.Push(fire);
                    fire.transform.SetParent(firePool_Transform);
                    SetSFXObject(fire);
                    return firePool.Pop();
                }
                return firePool.Pop();
            #endregion

            #region 가시
            case 3: // 가시
                if (spikePool.Count == 0) // 오브젝트풀에 오브젝트가 없을때
                {
                    // 오브젝트 생성해서 리턴
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
         * roomPrefabs[ 스테이지 - 1 , 방번호 - 1 ]
         * (-1 : 생성X)  (0 : 시작방)  (1 : 일반방)  (2 : 보스방)  (3 : 상점방)  (4 : 황금방) (5 : 저주방)
         * 
         * doorPrefabs[]
         * (0 : 일반방)  (1 : 보스방)  (2 : 상점방)  (3 : 골드방)  (4 : 저주방)
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
        roomList = null; // 생성된 방 오브젝트 배열 초기화
        doors = new List<GameObject>(); // 생성된 문 오브젝트 배열 초기화

        // 생성되어있는 모든 방들을 삭제
        for(int i = 0; i < roomPool.childCount; i++)
        {
            Destroy(roomPool.GetChild(i).gameObject);
        }

        // 생성된 아이템 오브젝트 List 초기화
        if (itemList == null)
        {
            itemList = new List<GameObject>();
        }

        else 
        {
            // 생성되어있는 아이템이있을때 전부 삭제 후 초기화
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
                int roomNum = GameManager.instance.stageGenerate.stageArr[i, j]; // 스테이지 구조에서 방 번호

                // 비어있는방
                if (roomNum == 0)
                {
                    roomPos += new Vector3(15, 0, 0);
                    continue;
                }
                // 그외

                // 방 오브젝트 생성
                GameObject room = Instantiate(roomPrefabs[stage - 1, roomNum - 1], roomPos, Quaternion.identity) as GameObject; // 방 오브젝트를 생성.
                SetSFXObject(room); // 해당 방을 Sound Manager의 SFXObject로 추가
                roomList[i, j] = room; 
                roomList[i, j].GetComponent<Room>().isClear = false; // 해당 방을 미클리어 상태로 전환

                // (4 상점) (5 황금) (6 저주) (7 시작) 인 경우 바로 클리어 상태로 전환
                if (roomNum == 4 || roomNum == 5 || roomNum == 6 || roomNum == 1)
                    roomList[i, j].GetComponent<Room>().isClear = true;
                

                // 방 오브젝트 생성
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

            // 문 선택
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

            // 도어 입장 데미지.
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
        // 현재방이 보스,상점,황금,저주 일때
        if (3 <= roomNum && roomNum <= 6)
        {
            doorNum = roomNum - 2;
        }
        // 현재방이 시작, 일반 일때
        else
        {
            // 다음방이 보스,상점,황금,저주 일때
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

                if (pNum == 5) // 몬스터 오브젝트일때
                {
                    // 랜덤한 일반몬스터를 반환받음.
                    GameObject enemy = enemyGenerate.GetEnemy();
                    enemy.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]);
                    enemy.transform.localPosition = new Vector3(0, 0, 0);
                    enemy.GetComponent<Enemy>().roomInfo = roomList[y, x];
                    roomList[y, x].GetComponent<Room>().enemis.Add(enemy); // 해당 방의 몬스터리스트에 추가

                    // sfx 사운드 조절을 위한 오브젝트 저장
                    SetSFXObject(enemy);
                }

                // 그 외
                else
                {
                    if (pNum == 10) // 플레이어 오브젝트일때
                    {
                        Transform pos = roomList[y, x].GetComponent<Room>().roomObjects[idx].transform;
                        GameManager.instance.playerObject.transform.position = pos.position;
                    }

                    // 그외
                    else
                    {
                        GameObject obstacle = Instantiate(objectPrefabs[pNum - 1]) as GameObject; // 오브젝트 생성
                        obstacle.transform.SetParent(roomList[y, x].GetComponent<Room>().roomObjects[idx]); // 오브젝트 위치 설정
                        obstacle.transform.localPosition = new Vector3(0, 0, 0); // 오브젝트 위치 설정

                        if (pNum == 7) // 황금방 아이템 테이블
                        {
                            obstacle.GetComponent<GoldTable>().SetRoomInfo(roomList[y,x]);
                        }
                        else if(pNum == 6) // 상점방 아이템 테이블
                        {
                            obstacle.GetComponent<ShopTable>().SetRoomInfo(roomList[y, x]);
                        }

                        // sfx 사운드 조절을 위한 오브젝트 저장
                        SetSFXObject(obstacle);
                    }

                }
                idx++;
            }
        }    
    }

    void SetSFXObject(GameObject obj)
    {
        // sfx 사운드 조절을 위한 오브젝트 저장
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
