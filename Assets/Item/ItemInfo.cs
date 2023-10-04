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
                ItemManager.instance.PassiveItems[itemCode] = true; // �̺��� -> ���� �� ���� 
                UseItem();

                gameObject.transform.SetParent(collision.gameObject.GetComponent<PlayerController>().itemPosition);
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                Destroy(gameObject.GetComponent<Rigidbody2D>());

                StartCoroutine(collision.gameObject.GetComponent<PlayerController>().GetItemTime());
            }
        }
    }

    public virtual void UseItem() { Debug.Log("������ ����!"); }
}
