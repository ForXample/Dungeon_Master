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
    public void InitStart()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 向hero移动的动画，包括了设置走和站立的animation， 一定距离停下来
    /// </summary>
    /// <typeparam name="T">一般为enum类Animation Name</typeparam>
    /// <param name="cannotWalk">一般为PrimeActionsBool的返回值</param>
    /// <param name="idleAnimation">站立的动画</param>
    /// <param name="walkAnimation">行走的动画</param>
    /// <param name="currentAnimation">现在正在播放的动画，一般是current Animation</param>
    public void MoveTowardHero<T>(bool cannotWalk, T idleAnimation, T walkAnimation, T currentAnimation)
    {
        if (!cannotWalk)
        {
            /* 判断距离，一定距离就停止 */
            if (heroDistance > distanceOffset)
            {
                speed = defaultSpeed;
                ChangeAnimationStates(walkAnimation, currentAnimation);
            }
            else
            {
                speed = 0;
                ChangeAnimationStates(idleAnimation, currentAnimation);
            }
        }
        else
        {
            speed = 0;
        }

        rb.velocity = heroDirection.normalized * speed;
        Debug.Log(rb.velocity);
    }

    /*---- 动画相关 ---*/

    /// <summary>
    /// 父类动作选择辅助function，在子类中重写
    /// </summary>
    /// <returns>返回值用于setAnimation中</returns>
    protected virtual bool PrimeActionsBool()
    {
        return false;
    }

    /// <summary>
    /// 改变并且传递相应的动画到动画控制器
    /// </summary>
    /// <typeparam name="T">一般为AnimationName， 本地enum</typeparam>
    /// <param name="newAnimation">传入的新的动画</param>
    /// <param name="currentAnimation">原先播放的动画， 一般为本地AnimaitonName变量</param>
    public void ChangeAnimationStates<T>(T newAnimation, T currentAnimation)
    {
        //如果正在播放动画则直接返回
        if (currentAnimation.ToString() == newAnimation.ToString()) return;

        //核心function，直接播放对应名称动画
        animator.Play(newAnimation.ToString());

        //播放完赋值给现在动画
        currentAnimation = newAnimation;
    }

    /*---- 协程相关 ---*/

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
        animator.SetFloat("facingDir", Mathf.InverseLerp(0, 3, (float)facingDir));
    }
}