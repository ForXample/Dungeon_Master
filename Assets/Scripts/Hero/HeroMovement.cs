using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private enum Facing { up, down, left, right };
    private Facing facingDir = Facing.down;
    private Vector2 inputMovement;

    [Header("Hero的移动属性")]
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        inputMovement.x = Input.GetAxisRaw("Horizontal");
        inputMovement.y = Input.GetAxisRaw("Vertical");
        SetDiection();
    }

    private void FixedUpdate()
    {
        if(inputMovement.magnitude != 0) //hero正在移动
        {
            /*这里是一定需要传入的数值*/
            animator.SetLayerWeight(1, 1); //increase the weight of walking layer
            animator.SetFloat("xDir", inputMovement.x);

            /*这里的区别了是否按下了两个input， 同时提高了x轴移动的优先度*/
            if(inputMovement.magnitude <= 1.4)
            {
                animator.SetFloat("yDir", inputMovement.y); //这里是正常大小，范围在（-1.1）
            }
            else
            {
                animator.SetFloat("yDir", 2); //这里会传出一个大于默认范围的数值，让controller可以识别出
            }
            //Debug.Log(inputMovement.magnitude);
            //Debug.Log(Mathf.Sqrt(2));
        }

        else // hero 站立不动
        {
            animator.SetLayerWeight(1, 0);
            //print("standstill");
        }
        // 设置位移， can replace rb.position to transform.position
        rb.MovePosition(rb.position + inputMovement * speed * Time.fixedDeltaTime); 
    }

    public void SetDiection()
    {
        if(inputMovement.x > 0)
        {
            facingDir = Facing.right;
        }
        else
        {
            facingDir = Facing.left;
        }
        if(inputMovement.y > 0)
        {
            facingDir = Facing.up;
        }
        else
        {
            facingDir = Facing.down;
        }
    }


    
}
