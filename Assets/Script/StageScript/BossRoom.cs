using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoom : MonoBehaviour
{
    [Header("Room info")]
    [SerializeField] Enemy bossComponent;
    [SerializeField] bool spawnBoss = true;
    private Enemy bossObject;

    [Header("unity Setup")]
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject nextStageDoor;
    [SerializeField] GameObject bossHpUI;
    [SerializeField] Image bossHP;

    private void Update()
    {
        if (gameObject.GetComponent<Room>().isClear)
        {
            nextStageDoor.SetActive(true);
            bossHpUI.SetActive(false);
            
        }
        else 
        {
            nextStageDoor.SetActive(false);
            bossHpUI.SetActive(true);
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

                GameObject boss;
                // ��������
                boss = GameObject.Find("EnemyGenerate").GetComponent<EnemyGenerate>().GetBoss();

                // ����������Ʈ�� ������ �ڽĿ�����Ʈ�� ����
                boss.transform.SetParent(gameObject.transform);

                // ���� ������Ʈ ��ġ�� 0 0 0 ���� �ʱ�ȭ
                boss.transform.localPosition = new Vector3(0, 0, 0);

                // ����������Ʈ�� �������� Room ��ũ��Ʈ�� enemis�� �߰�.
                gameObject.GetComponent<Room>().enemis.Add(boss);
                bossComponent = boss.GetComponent<Enemy>();
                bossComponent.hpBarSlider = bossHP;

                gameObject.GetComponent<Room>().isClear = false;
            }
        }
    }
}
