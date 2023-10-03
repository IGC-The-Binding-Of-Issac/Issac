using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    [Header("unity Setup")]
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject boss;
    //[SerializeField] GameObject nextStageDoor;

    private void Update()
    {
        if(gameObject.GetComponent<Room>().isClear)
        {
            openNextStage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� ������ �����
        if(collision.gameObject.CompareTag("Player"))
        {
            // ��������
            boss = GameObject.Find("EnemyGenerate").GetComponent<EnemyGenerate>().GetBoss();
            // ����������Ʈ�� ������ �ڽĿ�����Ʈ�� ����
            boss.transform.SetParent(gameObject.transform); 
            // ����������Ʈ�� �������� Room ��ũ��Ʈ�� enemis�� �߰�.
            gameObject.GetComponent<Room>().enemis.Add(boss);

            // ����HP�� ����
        }
    }

    void openNextStage()
    {
        // ���� ���������� ����
        //nextStageDoor.SetActive(true);

        // ���� HP�� �������ֱ�
    }
}
