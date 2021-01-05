using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    /*---- 基础属性 ----*/
    private Rigidbody2D rb;
    private Animator animator;
    private AnimationName currentAnimation;

    /*---- ENUM属性类 ----*/

    /// <summary>
    /// 关于hero的朝向
    /// </summary>
    private enum Facing
    {
        up,
        right,
        down,
        left
    };

    /// <summary>
    /// 关于所有的animaton的名字，必须与controller上的保持一致
    /// </summary>
    private enum AnimationName
    {
        Hero_Idle,
        Hero_Walk,
        Hero_Dead,
        Hero_Attack,
        Hero_Defend
    };

    /// <summary>
    /// 所有的除行走和站立的动作种类，更新后要更新actionTypeNum的数量
    /// </summary>
    private enum ActionType
    {
        Attack,
        Dash,
        Defend
    };

    /// <summary>
    /// ActionType内的数量，用于建立bool数组
    /// </summary>
    private int actionTypeNum = 3;

    /*---- 动作布尔值相关----*/
    private bool isAttackPress;
    private bool isDashPress;
    private bool isDefendPress;
    private bool[] actionBoolArray;

    /*---- 移动相关 ----*/
    private Facing facingDir = Facing.down;
    private Vector2 inputMovement;
    private float heroFacingAngle;
    private float speed;

    /*---- 碰撞检测相关 ----*/
    private GameObject SwordUpDownCollider;
    private GameObject SwordRightLeftCollider;
    private GameObject hitbox;

    [Header("Hero的移动属性")]
    public float defaltSpeed;

    [Header("Hero的攻击动画属性")]
    public float startTime;
    public float exitTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = defaltSpeed;
        actionBoolArray = new bool[actionTypeNum];

        /* 攻击范围碰撞体相关 */
        SwordUpDownCollider = transform.GetChild(0).GetChild(0).gameObject;
        SwordRightLeftCollider = transform.GetChild(0).GetChild(1).gameObject;
        SwordUpDownCollider.SetActive(false);
        SwordRightLeftCollider.SetActive(false);
    }

    /// <summary>
    /// 所有与按键输入相关都进update
    /// </summary>
    private void Update()
    {
        GetAndSetDiection();

        /* 攻击按键是否按下判定 */
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttackPress = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashPress = true;
        }
    }

    /// <summary>
    /// 所有和物理，移动相关都进fixed update
    /// </summary>
    private void FixedUpdate()
    {
        Walking();
        Attack();
    }

    /// <summary>
    /// 设置Hero移动动画和移动距离
    /// </summary>
    private void Walking()
    {
        if (!actionBoolArray[(int)ActionType.Attack])
        {
            if (inputMovement.magnitude != 0)
            {
                ChangeAnimationStates(AnimationName.Hero_Walk);
            }
            else
            {
                ChangeAnimationStates(AnimationName.Hero_Idle);
            }
        }
        // 设置位移
        rb.velocity = inputMovement.normalized * speed;
    }

    /// <summary>
    /// 设置Hero攻击动画
    /// </summary>
    private void Attack()
    {
        if (isAttackPress)
        {
            isAttackPress = false;
            if (!actionBoolArray[(int)ActionType.Attack])
            {
                actionBoolArray[(int)ActionType.Attack] = true;
                ChangeAnimationStates(AnimationName.Hero_Attack);
                StartCoroutine(EnableCollider(ActionType.Attack));
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

                SwordUpDownCollider.transform.localRotation = Quaternion.Euler(180, 0, 0);
                SwordUpDownCollider.transform.position += new Vector3(0, -0.1f, 0);
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

                SwordRightLeftCollider.transform.localRotation = Quaternion.Euler(0, 180, 0);
                SwordRightLeftCollider.transform.position += new Vector3(0, 0.02f, 0);
                SwordRightLeftCollider.SetActive(true);

                return SwordRightLeftCollider;
        }
        Debug.LogError("ATTACK cannot find direction");
        return null;
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

    /*--- 辅助functions ---*/

    /// <summary>
    /// 将int转换为bool
    /// </summary>
    /// <param name="input">输入的int</param>
    /// <returns></returns>
    private bool IntToBool(int input)
    {
        if (input == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    ///在一定时间后打开指定collider， 同时根据
    /// </summary>
    /// <param name="actiontype">执行的动作类型</param>
    /// <returns></returns>
    private IEnumerator EnableCollider(ActionType actiontype)
    {
        yield return new WaitForSeconds(startTime);
        switch (actiontype)
        {
            case ActionType.Attack:
                hitbox = AttackDirction();
                break;
        }
        StartCoroutine(DisableCollider(hitbox, actiontype));
    }

    /// <summary>
    ///在一定时间后关闭指定collider
    /// </summary>
    /// <param name="collider">在一定时间后disable的collider</param>
    /// <param name="actiontype">关闭的condition name（string)</param>
    /// <returns></returns>
    private IEnumerator DisableCollider(GameObject collider, ActionType actiontype)
    {
        yield return new WaitForSeconds(exitTime);
        collider.SetActive(false);

        //还原collieder的初始属性和bool值
        collider.transform.position = rb.position;
        collider.transform.rotation = Quaternion.identity;
        actionBoolArray[(int)actiontype] = false;
    }

    /// <summary>
    /// 根据x轴y轴设定朝向
    /// </summary>
    public void GetAndSetDiection()
    {
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("xDir", inputMovement.x);
        animator.SetFloat("yDir", inputMovement.y);
        inputMovement = inputMovement.normalized;

        /* 通过角度判断 */
        Vector2 offset = new Vector2(0.000001f, 0.000001f); //本来想用Vector.zero的，但是输出的angle只会是0，所以就只能取一个接近于0的点
        heroFacingAngle = Vector2.SignedAngle(offset, inputMovement.normalized);
        //Debug.Log(heroFacingAngle);
        if (heroFacingAngle == 135)
        {
            facingDir = Facing.left;
        }
        else if (heroFacingAngle == -45)
        {
            facingDir = Facing.right;
        }
        else if (heroFacingAngle == 45)
        {
            facingDir = Facing.up;
        }
        else if (heroFacingAngle == -135)
        {
            facingDir = Facing.down;
        }
        else
        {
            //Debug.LogError("wrong facing");
        }
        animator.SetFloat("facingDir", Mathf.InverseLerp(0, 3, (float)facingDir));
    }
}