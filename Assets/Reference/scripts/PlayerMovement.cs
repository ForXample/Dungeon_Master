using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float defalt_speed;
    public float runSpeedScale;
    private float speed;
    private Vector2 direction;

    //for SetAnimatorMovement fuction
    private Animator animator;

    //for dashing 
    public float dashRange;
    private enum Facing {up, down, left, right};
    private Facing facingDir = Facing.down;
    private Vector2 targetPosition;

    void Start() 
    {
        speed = defalt_speed;//set defalt walking speed
        animator = GetComponent<Animator>();
        animator.gameObject.SetActive(true);
        //check whether animator is wrong
        //if(animator.gameObject.activeSelf == false)
        //{
        //    Debug.LogError("no animator controller");
        //}
    }

    private void FixedUpdate()
    {
        TakeInput();
        Move();
        //Debug.Log(direction.magnitude);
    }

    private void Move()
    {
                                                                 //check whether is moving
        if (direction.x != 0 | direction.y != 0)
        {
            if(direction.magnitude <= 1.4)                      //成对角线的时候，vector的大小会大于1.41 (根号2）
            {
                animator.SetLayerWeight(1, 1);
                SetAnimatiorMovement(direction);
            }
        }
        else 
        {
            animator.SetLayerWeight(1,0);                        // is not walking
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) //speed up
        {
            speed  = speed * runSpeedScale;
            //Debug.Log(speed);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //speed down
        {
            speed = defalt_speed;
            //Debug.Log(speed);
        }

        transform.Translate(direction * speed * Time.deltaTime); //calculation based on time
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))//goes up
        {
            direction += Vector2.up;
            facingDir = Facing.up;
        }
        if (Input.GetKey(KeyCode.S))//goes down
        {
            direction += Vector2.down;
            facingDir = Facing.down;
        }
        if (Input.GetKey(KeyCode.A))//goes left
        {
            direction += Vector2.left;
            facingDir = Facing.left;
        }
        if (Input.GetKey(KeyCode.D))//goes right
        {
            direction += Vector2.right;
            facingDir = Facing.right;
        }

        //dashing 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("DASH");
            //Vector2 currentPos = transform.position;
            targetPosition = Vector2.zero;  

            if(facingDir == Facing.up)
            {
                targetPosition.y = 1;
            }
            else if(facingDir == Facing.down)
            {
                targetPosition.y = -1;
            }
            else if (facingDir == Facing.right)
            {
                targetPosition.x = 1;
            }
            else if(facingDir == Facing.left)
            {
                targetPosition.x = -1;
            }
            transform.Translate(targetPosition * dashRange); 
        }
    }
    
    private void SetAnimatiorMovement(Vector2 direction)
    {
        animator.SetFloat("Dir_x", direction.x);
        animator.SetFloat("Dir_y", direction.y);
        //test code 
        //print(animator.GetFloat("Dir_x"));
        //print(animator.GetFloat("Dir_y"));
        //Debug.Log(animator.GetFloat("Dir_x"));
        //Debug.Log(animator.GetFloat("Dir_y"));
    }
}
    