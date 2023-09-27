using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [Header("Unity Setup")]
    [SerializeField] GameObject[] enemyPrefabs;

    // �����ϰ� ���� ��ȯ.
    public GameObject GetEnemy()
    {
        int rd = Random.Range(0, enemyPrefabs.Length);

        GameObject enemy;
        enemy = Instantiate(enemyPrefabs[rd]) as GameObject;
        return enemy;
    }
}