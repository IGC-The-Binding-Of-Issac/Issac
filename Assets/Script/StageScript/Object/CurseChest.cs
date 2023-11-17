using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class CurseChest : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] Sprite openChestSprite;
    [SerializeField] Sprite closeChestSprite;
    Room roomInfo;

    private void Start()
    {
        closeChestSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void ResetObject()
    {
        // �ʱ�ȭ
        gameObject.GetComponent<SpriteRenderer>().sprite = closeChestSprite;
        gameObject.layer = 15;

        // ������Ʈ ����.
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ǯ���ƿ� �浹��
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = openChestSprite; // �������� �̹����� ����
            OpenChest(); // ���� �Ǵ� �нú� ������ ���
            openChestSound(); // ���� ���� ���� ����
            StartCoroutine(StopChest()); // ���� �и� ����!
        }
    }

    IEnumerator StopChest()
    {
        gameObject.layer = 16;
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

    }

    void OpenChest()
    {
        int rd = Random.Range(0, 10000);
        if(rd % 2 == 0)
        {
            GameObject it = Instantiate(ItemManager.instance.itemTable.DropPassive(), transform.position, Quaternion.identity) as GameObject;
            GameManager.instance.roomGenerate.itemList.Add(it);
            return;
        }

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        // �ĸ� �Ǵ� �Ź��� ������ ���� 1�� ����
        int rd = Random.Range(0, 1000);
        int randomEnemyIndex;
        if (rd % 2 == 0)
            randomEnemyIndex = 0;
        else
            randomEnemyIndex = 5;

        // �ĸ� �Ǵ� �Ź̸� ���� ����.
        GameObject enemy = GameManager.instance.roomGenerate.enemyGenerate.GetEnemy(randomEnemyIndex);
        enemy.transform.SetParent(roomInfo.transform);
        enemy.transform.position = gameObject.transform.position;
        enemy.GetComponent<TEnemy>().roomInfo = roomInfo.gameObject;

        // �ش� ���� ���͸���Ʈ�� �߰�
        roomInfo.GetComponent<Room>().enemis.Add(enemy);

        // ���� �������� �ش�濡 ���Ͱ� �����ϱ⶧����
        // �ش� ���� Ŭ���� ���θ� false�� ����
        roomInfo.isClear = false; 

        // sfx ���� ������ ���� ������Ʈ ����
        GameManager.instance.roomGenerate.SetSFXDestoryObject(enemy);
    }

    public void SetRoomInfo(GameObject room)
    {
        roomInfo = room.GetComponent<Room>();
    }

    void openChestSound()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}
