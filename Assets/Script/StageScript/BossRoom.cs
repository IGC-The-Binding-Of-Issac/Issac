using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRoom : MonoBehaviour
{
    [Header("Room info")]
    [SerializeField] TEnemy bossComponent;
    [SerializeField] bool spawnBoss = true;

    [Header("unity Setup")]
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject nextStageDoor;
    [SerializeField] GameObject bossHpUI;
    [SerializeField] Image bossHP;
    private void Update()
    {
        // ������ ������ ����
        if(!spawnBoss)
        {
            // ���� Ŭ����Ǹ�.
            if (gameObject.GetComponent<Room>().isClear)
            {
                nextStageDoor.SetActive(true);
                bossHpUI.SetActive(false);

                // ������ ���� ����
                GameObject reward = Instantiate(ItemManager.instance.goldTable) as GameObject;
                reward.transform.SetParent(gameObject.transform);
                reward.transform.localPosition = new Vector3(0, -1.3f, 0);

                reward.GetComponent<GoldTable>().SpawnItem(true);

                Destroy(this); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� ������ �����
        if(collision.gameObject.CompareTag("Player"))
        {
            if(spawnBoss)
            {
                gameObject.GetComponent<Room>().isClear = false;

                // ���� ����� ����
                spawnBoss = false;

                GameObject boss;
                // ��������
                boss = GameManager.instance.roomGenerate.enemyGenerate.GetBoss();
                //boss = GameObject.Find("EnemyGenerate").GetComponent<EnemyGenerate>().GetBoss();

                // ����������Ʈ�� ������ �ڽĿ�����Ʈ�� ����
                boss.transform.SetParent(gameObject.transform);

                // ���� ������Ʈ ��ġ�� 0 0 0 ���� �ʱ�ȭ
                boss.transform.localPosition = new Vector3(0, 0, 0);

                // ����������Ʈ�� �������� Room ��ũ��Ʈ�� enemis�� �߰�.
                gameObject.GetComponent<Room>().enemis.Add(boss);
                boss.GetComponent<TEnemy>().roomInfo = gameObject;
                bossComponent = boss.GetComponent<TEnemy>();
                bossComponent.hpBarSlider = bossHP;


                // ���ݱ� / ���� ü�¹� ����
                nextStageDoor.SetActive(false);
                bossHpUI.SetActive(true);
            }
        }
    }
}
