using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moter : Top_Fly
{
    [SerializeField ]GameObject attackFly;

    void Start()
    {
        Fly_Move_InitialIze();

        playerInRoom = false;
        dieParameter = "isDie";

        //Enemy
        hp = 3f;
        sight = 5f;
        moveSpeed = 0.5f;
        waitforSecond = 0.5f;

        maxhp = hp;
    }

    private void Update()
    {

        justTrackingPlayerPosi = GameObject.FindWithTag("Player").transform;
        if (justTrackingPlayerPosi == null)
            return;

        if (playerInRoom)
        {
            Move();
        }
    }


    private void OnDestroy()
    {
        if(hp <= 0.1f) // ������Ʈ�� Destory�� �ɶ� �׳� Destory�� �Ǵ°���. HP�� ���� �Ҿ� Destory�Ǵ°��� Ȯ��.
        {
            GenerateAttackFly();
            GenerateAttackFly();
        }
    }

    public override void Move()
    {
        Tracking(justTrackingPlayerPosi);
    }

    void GenerateAttackFly() 
    {
        GameObject obj = Instantiate(attackFly, transform.position, Quaternion.identity) as GameObject;

        // SoundManage�� sfxObject�� �߰�.
        if (obj.GetComponent<AudioSource>() != null)
            SoundManager.instance.sfxObjects.Add(obj.GetComponent<AudioSource>());

        roomInfo.GetComponent<Room>().enemis.Add(obj);
    }

}
