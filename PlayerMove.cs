using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer ;
    Animator animator;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    //Stop Speed
    void Update() //지속적으로 호출, 보통 단발적인 키 입력을 다룰 때 사용
    {
        if(Input.GetButtonUp("Horizontal"))
        {  
            //rigid.velocity.x 는 방향뿐만아니라 크기까지 포함하는 표현
            //크기를 단위로 만들어주어야함.
            //normalized 를 쓰면 벡터 크기를 1로 만들 수 있음.
            //GetAxisRaw와 거의 비슷..
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f , rigid.velocity.y);
        }

        //direction change
        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1 ;
        }

        //Animation transition
        if(Mathf.Abs(rigid.velocity.x) < 0.5)
            animator.SetBool("isWalking",false);
        else
            animator.SetBool("isWalking",true);
    }

    //Max Speed
    void FixedUpdate() //기본적으로 1초에 50회 호출
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
    //AddForce : 힘 추가 1초동안 꾹 누르면 힘이 50번 주어짐
    
    if(rigid.velocity.x > maxSpeed) // right Max Speed
    //velocity : 속도
    {
        rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
    }

    else if(rigid.velocity.x < maxSpeed*(-1)) // Left Max Speed
    //velocity : 속도
    {
        rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
    
    }

    }
}
