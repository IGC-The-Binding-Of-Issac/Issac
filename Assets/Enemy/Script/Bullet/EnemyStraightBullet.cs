using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStraightBullet : Enemy_Bullet
{
    /// <summary>
    /// �����ϴ� �Ѿ�
    /// </summary>
    void Start()
    {
        isCoru = true;
        ani         = GetComponent<Animator>();
        waitForDest = 0.5f;
        bulletSpeed = 5f;
    }

}
