using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePoop : ActiveInfo
{
    [SerializeField]
    GameObject goldenPoop;

    public void Awake()
    {
        base.Start();
        SetActiveItem(19, 0);
        SetActiveString("Ȳ�ݶ�",
                        "�������� ��ܿ�",
                        "��� �� �÷��̾ ��ġ�� �ڸ��� Ȳ�� ���� �Ѵ�.");
    }

    public override void UseActive()
    {
        if (canUse)
        {
            Transform poopTransform = GameManager.instance.playerObject.GetComponent<Transform>();
            GameObject goldPoop = Instantiate(goldenPoop, new Vector3(poopTransform.position.x, poopTransform.position.y, 0), Quaternion.identity) as GameObject;

            goldPoop.transform.SetParent(GameManager.instance.roomGenerate.roomPool.GetChild(0).transform); // �׳� �ƹ��濡�ٰ� ���� ����α�.
            SoundManager.instance.sfxObjects.Add(goldPoop.GetComponent<AudioSource>());
        }
    }
}
