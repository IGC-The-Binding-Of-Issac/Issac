using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region singleton
    public static ItemManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    public int item_Activated_Count; //�÷��̾ �����̽��ٸ� ���� Ƚ��, 1ȸ�� �������� ���� �ɷ�ġ ���� (��Ƽ�� ������)
    public int inventory = 0; // ��Ʈ�� ��ȯ �ʿ�. 
    public int coinCount = 0; // ���� ���� ���� 
    public int bombCount = 0; // ��ź ���� ����
    public int keyCount = 0; // ���� ���� ����


    [Header("Unity Setup")]
    public GameObject bombPrefab;
}
