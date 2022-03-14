using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    private float xVelocity;
    private Rigidbody2D rb;

    public float speed; //移动速度
    public float jumpForce; //跳跃力

    public Transform groudCheck; //地面检测点
    public LayerMask ground;  //判断在地面图层

    public bool isGround; //判断是否在地上
    public bool isJump; //判断是否跳跃

    private bool jumpPressed; //跳跃键是否被按下
    private int jumpCount; //跳跃总次数

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetButtonDown("Jump") &&  jumpCount > 0)
        {
            jumpPressed = true;
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        isGround = Physics2D.OverlapCircle(groudCheck.position, 0.01f, ground); // 检测是否在地上 判定距离看需求改变
        Jump();
    }

    //人物移动代码
    void PlayerMovement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);    //人物横向移动
        
        //人物面朝方向
        if(xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);   //尽量用Vector3
        }
    }

    void Jump()
    {
        if(isGround)
        {
            jumpCount = 2; //二段跳
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if(jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
    }
}

