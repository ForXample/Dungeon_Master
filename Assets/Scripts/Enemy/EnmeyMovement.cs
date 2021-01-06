using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected enum Facing
    {
        up,
        right,
        down,
        left
    };

    /*---- 基础属性 ----*/
    protected Rigidbody2D rb;
    protected Animator animator;
    protected float heroDistance;
    protected float speed;
    protected Facing facingDir;

    /*---- 辅助变量----*/
    private Vector2 heroDirection;
    private float heroAngle;

    [Header("敌人移动基础属性")]
    public GameObject hero;
    public float defaultSpeed;
    public float distanceOffset;
    public float attackOffset;

    /// <summary>
    /// 用于给子类调取start
    /// </summary>
    public void initStart()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 向hero的位置移动
    /// </summary>
    /// <param name="keepDistanceRadias">与hero保持的距离</param>
    public void MoveTowardHero(bool isWalk)
    {
        /* 判断距离，一定距离就停止 */
        if (heroDistance > distanceOffset)
        {
            speed = defaultSpeed;
            isWalk = true;
        }
        else
        {
            speed = 0;
            isWalk = false;
        }

        rb.velocity = heroDirection.normalized * speed;
    }

    /* 设定动画状态机 */

    /* 得到敌人的面向位置并且发送给动画状态机 */

    protected void EnemyFacingDirection()
    {
        heroDirection = (hero.transform.position - transform.position);
        heroDistance = heroDirection.magnitude;
        /*通过角度判断*/
        Vector2 offset = new Vector2(0.000001f, 0.000001f); //本来想用Vector.zero的，但是输出的angle只会是0，所以就只能取一个接近于0的点
        heroAngle = Vector2.SignedAngle(offset, heroDirection.normalized);
        //Debug.Log(heroAngle);
        if (heroAngle >= 90 && heroAngle <= 180)
        {
            facingDir = Facing.left;
            animator.SetFloat("xDir", -1);
            animator.SetFloat("yDir", 0);
        }
        else if (heroAngle >= -90 && heroAngle <= 0)
        {
            facingDir = Facing.right;
            animator.SetFloat("xDir", 1);
            animator.SetFloat("yDir", 0);
        }
        else if (heroAngle < 90 && heroAngle > 0)
        {
            facingDir = Facing.up;
            animator.SetFloat("xDir", 0);
            animator.SetFloat("yDir", 1);
        }
        else if (heroAngle >= -180 && heroAngle <= -90)
        {
            facingDir = Facing.down;
            animator.SetFloat("xDir", 0);
            animator.SetFloat("yDir", -1);
        }
        else
        {
            Debug.LogError("wrong facing");
        }
        //print(facingDir);
    }
}