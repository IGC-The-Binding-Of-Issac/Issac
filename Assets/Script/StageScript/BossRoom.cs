using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [Header("Room info")]
    [SerializeField] GameObject boss;

    [Header("unity Setup")]
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject nextStageDoor;

    [SerializeField] bool spawnBoss = true;
    private void Update()
    {
        if (gameObject.GetComponent<Room>().isClear)
        {
            nextStageDoor.SetActive(true);
        }
        else 
        {
            nextStageDoor.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� ������ �����
        if(collision.gameObject.CompareTag("Player"))
        {
            if(spawnBoss)
            {
                // ���� ����� ����
                spawnBoss = false;

                // ��������
                boss = GameObject.Find("EnemyGenerate").GetComponent<EnemyGenerate>().GetBoss();

                // ����������Ʈ�� ������ �ڽĿ�����Ʈ�� ����
                boss.transform.SetParent(gameObject.transform);

                // ���� ������Ʈ ��ġ�� 0 0 0 ���� �ʱ�ȭ
                boss.transform.localPosition = new Vector3(0, 0, 0);

                // ����������Ʈ�� �������� Room ��ũ��Ʈ�� enemis�� �߰�.
                gameObject.GetComponent<Room>().enemis.Add(boss);

                gameObject.GetComponent<Room>().isClear = false;
                // ����HP�� ����
                // ���� �ʿ�.
            }
        }
    }
}
