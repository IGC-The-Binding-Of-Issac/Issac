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

    [Header("Drop Item State")]
    public int coinCount = 0; // ���� ���� ���� 
    public int bombCount = 0; // ��ź ���� ����
    public int keyCount = 0; // ���� ���� ����

    [Header("Passive Item State")]
    public bool[] PassiveItems; // �нú� ������ ���� ��Ȳ
    /*
     * 1. �����ϰ� ���� �� �ش� �������� ȿ���� �������� �����͸� ��������.
     * 2. ���� ��Ȳ�� ���� �߰� �ɷ�ġ�� PlayerManager ���ȿ� ���������.
     * 3. ���� ��Ȳ�� ���� �ó��� ȿ���� �ۼ��������.
     * 4. ���� ��Ȳ�� ���� �÷��̾��� �̹��� ���ҽ��� �����������.(����)
     * 5. ���� ��Ȳ�� ���� ������ �ߺ� ��� ����
    */

    [Header("unique Item State")]
    public GameObject ActiveItem; // ���� �������� ��Ƽ�� ������
    public GameObject TrinketItem; // ���� �������� ��ű� ������
    
    [Header("Unity Setup")]
    public GameObject bombPrefab; // ��ġ�ϴ� ��ź ������Ʈ ������
    public ItemTable itemTable; // ������ ������� ��ũ��Ʈ

    private void Start()
    {
        PassiveItems = new bool[100];
    }


}
