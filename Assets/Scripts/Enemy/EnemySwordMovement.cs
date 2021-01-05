using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordMovement : EnemyMovement
{
    [Header("Sword类敌人移动属性")]
    public float attackOffect; //与hero之间的攻击距离

    private void Start()
    {
        initStart();
    }

    private void FixedUpdate()
    {
        MoveTowardHero(attackOffect);
    }
}