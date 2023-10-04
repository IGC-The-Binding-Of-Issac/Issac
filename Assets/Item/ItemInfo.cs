using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public int itemCode;
    public string itemName;

    public float item_Damage; //���ݷ� ���� (+, -)
    public float item_DamageMulti; //���ݷ� ���� (*, /)
    public float item_Hp; //ü�� ���� (+, -)
    public float item_MaxHP; //�ִ� ü�� ���� (+, -)
    public float item_AttackSpeed; //���ݼӵ� ���� (+, -)
    public float item_Range; //��Ÿ� ���� (+, -)
    public float item_MoveSpeed; //�̵��ӵ� ���� (+, -)
    public float item_ShotSpeed; //�����ӵ� ���� (+, -)
    public float item_Luck; //��� ���� (+, -)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // �������� ����������������.
            if(!ItemManager.instance.PassiveItems[itemCode])
            {
                // �÷��̾� ������ ȹ�� �ִϸ��̼ǽ����ϴ� �ڵ� �߰����ֱ�.
                ItemManager.instance.PassiveItems[itemCode] = true; // �̺��� -> ���� �� ���� 
                UseItem();
            }
                
        }
    }

    public virtual void UseItem() { Debug.Log("������ ����!"); }
}
