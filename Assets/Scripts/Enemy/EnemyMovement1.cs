using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("敌人移动基础属性")]
    public GameObject hero;
    public float defaultSpeed;

    private enum Facing { up, down, left, right };

    private Facing facingDir = Facing.down;
    private Vector2 heroDirection;
    private float heroAngle;
    private float heroDistance;
    private Rigidbody2D rb;
    private Animator animator;
    private bool whetherHeroStop;
    private float speed;

    /* 用于给子类调取start */

    public void initStart()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /* 向hero的位置移动 */

    public void MoveTowardHero(float keepDistanceRadias)
    {
        /* 判断距离，一定距离就停止 */
        heroDirection = (hero.transform.position - transform.position);
        heroDistance = heroDirection.magnitude;

        if (heroDistance > keepDistanceRadias)
        {
            speed = defaultSpeed;
            whetherHeroStop = false;
        }
        else
        {
            speed = 0;
            whetherHeroStop = true;
        }
        //Debug.Log(speed);
        //Debug.Log(heroDirection);

        rb.MovePosition(rb.position + heroDirection * speed * Time.fixedDeltaTime);
        AnimationSetter();
    }

    /* 设定动画状态机 */

    private void AnimationSetter()
    {
        if (whetherHeroStop)
        {
            animator.SetLayerWeight(1, 0);
            EnemyFacingDirection();
            //print("standstill");
        }
        else
        {
            animator.SetLayerWeight(1, 1);
            EnemyFacingDirection();
            //print("fllow");
        }
    }

    /* 得到敌人的面向位置并且发送给动画状态机 */

    private void EnemyFacingDirection()
    {
        /* 方法2 通过角度判断 */
        Vector2 offset = new Vector2(0.000001f, 0.000001f); //本来想用Vector.zero的，但是输出的angle只会是0，所以就只能取一个接近于0的点
        heroAngle = Vector2.SignedAngle(offset, heroDirection.normalized);
        //Debug.Log(heroAngle);
        if (heroAngle >= 90 && heroAngle <= 180)
        {
            facingDir = Facing.left;
        }
        else if (heroAngle >= -90 && heroAngle <= 0)
        {
            facingDir = Facing.right;
        }
        else if (heroAngle < 90 && heroAngle > 0)
        {
            facingDir = Facing.up;
        }
        else if (heroAngle >= -180 && heroAngle <= -90)
        {
            facingDir = Facing.down;
        }
        else
        {
            Debug.LogError("wrong facing");
        }
        //print(facingDir);

        /* 赋值给动画状态机 */
        switch (facingDir)
        {
            case Facing.right:
                animator.SetFloat("xDir", 1);
                animator.SetFloat("yDir", 0);
                break;

            case Facing.left:
                animator.SetFloat("xDir", -1);
                animator.SetFloat("yDir", 0);
                break;

            case Facing.up:
                animator.SetFloat("xDir", 0);
                animator.SetFloat("yDir", 1);
                break;

            case Facing.down:
                animator.SetFloat("xDir", 0);
                animator.SetFloat("yDir", -1);
                break;
        }
    }
}