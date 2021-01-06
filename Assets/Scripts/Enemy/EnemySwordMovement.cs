using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordMovement : EnemyMovement
{
    /*---- 碰撞检测相关 ----*/
    private GameObject SwordUpDownCollider;
    private GameObject SwordRightLeftCollider;
    private GameObject hitbox;

    private enum AnimationName
    {
        Enemy_S_Idle,
        Enemy_S_Walk,
        Enemy_S_Dead,
        Enemy_S_Attack,
        Enemy_S_Defend
    };

    private int actionTypeNum;
    private bool[] actionBoolArray;

    private AnimationName currentAnimation;
    private bool isAttack;
    private bool isDefend;

    [Header("Sword类敌人移动属性")]
    public float example;

    [Header("Sword类敌人动画属性")]
    public float startTime;
    public float exitTime;

    private void Start()
    {
        actionBoolArray = new bool[actionTypeNum];
        initStart();

        /* 攻击范围碰撞体相关 */
        SwordUpDownCollider = transform.GetChild(0).GetChild(0).gameObject;
        SwordRightLeftCollider = transform.GetChild(0).GetChild(1).gameObject;
        SwordUpDownCollider.SetActive(false);
        SwordRightLeftCollider.SetActive(false);
    }

    private void Update()
    {
        EnemyFacingDirection();
        AttackBool();
    }

    private void FixedUpdate()
    {
        MoveTowardHero(actionBoolArray[(int)AnimationName.Enemy_S_Walk]);
        setAnimation();
    }

    /*---- 攻击相关functions ----*/

    private void AttackBool()
    {
        if (heroDistance <= distanceOffset)
        {
            isAttack = true;
        }
    }

    private void Attack()
    {
        if (isAttack)
        {
            isAttack = false;
            if (!actionBoolArray[(int)AnimationName.Enemy_S_Attack])
            {
                actionBoolArray[(int)AnimationName.Enemy_S_Attack] = true;
                ChangeAnimationStates(AnimationName.Enemy_S_Attack);
            }
        }
    }

    /// <summary>
    /// 作为给coroutine的子function，关于collid box的具体调整参数在这里
    /// </summary>
    /// <returns>返回值是一个collider，用于给DisableCollider关闭collider</returns>
    private GameObject AttackDirction()
    {
        switch (facingDir)
        {
            case Facing.down:
                /* 设置collided box 变形 和 配合动画打开关闭武器攻击检测 */
                //向下没有变形数据,重置为初始

                SwordUpDownCollider.transform.localRotation = Quaternion.identity;
                SwordUpDownCollider.transform.position = rb.position;
                SwordUpDownCollider.SetActive(true);

                return SwordUpDownCollider;

            case Facing.up:
                /* 设置collided box 变形 和 配合动画打开关闭武器攻击检测 */
                //向上变形数据：  Routation.180(x) + offset.y.-0.1
                //调正摄像机后第二次数据： Routation.180(x) + offset.y.-0.3

                SwordUpDownCollider.transform.localRotation = Quaternion.Euler(180, 0, 0);
                SwordUpDownCollider.transform.position += new Vector3(0, -0.3f, 0);
                SwordUpDownCollider.SetActive(true);

                return SwordUpDownCollider;

            case Facing.right:
                /* 设置collided box 变形 和 配合动画打开关闭武器攻击检测 */
                //向右没有变形数据,重置为初始

                SwordRightLeftCollider.transform.localRotation = Quaternion.identity;
                SwordRightLeftCollider.transform.position = rb.position;
                SwordRightLeftCollider.SetActive(true);

                return SwordRightLeftCollider;

            case Facing.left:
                /* 设置collided box 变形 和 配合动画打开关闭武器攻击检测 */
                //向左变形数据：  Routation.180(y) + offset.y.0.02
                //调正摄像机后第二次数据： Routation.180(y) + offset.y.0.02, x.0.03

                SwordRightLeftCollider.transform.localRotation = Quaternion.Euler(0, 180, 0);
                SwordRightLeftCollider.transform.position += new Vector3(0, 0.02f, 0);
                SwordRightLeftCollider.SetActive(true);

                return SwordRightLeftCollider;
        }
        Debug.LogError("ATTACK cannot find direction");
        return null;
    }

    /*---- 碰撞体相关function ----*/

    /// <summary>
    ///在一定时间后打开指定collider， 同时根据
    /// </summary>
    /// <param name="actiontype">执行的动作类型</param>
    /// <returns></returns>
    private IEnumerator EnableCollider(AnimationName animation)
    {
        yield return new WaitForSeconds(startTime);
        switch (animation)
        {
            case AnimationName.Enemy_S_Attack:
                hitbox = AttackDirction();
                break;
        }
        StartCoroutine(DisableCollider(hitbox, animation));
    }

    /// <summary>
    ///在一定时间后关闭指定collider
    /// </summary>
    /// <param name="collider">在一定时间后disable的collider</param>
    /// <param name="actiontype">关闭的condition name（string)</param>
    /// <returns></returns>
    private IEnumerator DisableCollider(GameObject collider, AnimationName animation)
    {
        yield return new WaitForSeconds(exitTime);
        collider.SetActive(false);

        //还原collieder的初始属性和bool值
        collider.transform.position = rb.position;
        collider.transform.rotation = Quaternion.identity;
        actionBoolArray[(int)animation] = false;
    }

    /*---- 动画相关function集合 ----*/

    /// <summary>
    /// 用于更新walk等第二优先级动画
    /// 有新的actiuon动画增加就需要再此更新返回值
    /// </summary>
    /// <returns>返回应用or链接</returns>
    private bool ActionsBool()
    {
        return (actionBoolArray[(int)AnimationName.Enemy_S_Attack] ||
               actionBoolArray[(int)AnimationName.Enemy_S_Defend]);
    }

    /// <summary>
    /// 总的调用动画的控制器
    /// </summary>
    private void setAnimation()
    {
        if (ActionsBool())
        {
        }
        else
        {
            /* 第二优先级动画 */
            if (actionBoolArray[(int)AnimationName.Enemy_S_Walk])
            {
                ChangeAnimationStates(AnimationName.Enemy_S_Walk);
            }
            else
            {
                ChangeAnimationStates(AnimationName.Enemy_S_Idle);
            }
        }
    }

    /// <summary>
    /// 更换动画状态
    /// </summary>
    /// <param name="newAnimation">需要更换的动画名称</param>
    private void ChangeAnimationStates(AnimationName newAnimation)
    {
        //如果正在播放动画则直接返回
        if (currentAnimation == newAnimation) return;

        //核心function，直接播放对应名称动画
        animator.Play(newAnimation.ToString());

        //播放完赋值给现在动画
        currentAnimation = newAnimation;
    }
}