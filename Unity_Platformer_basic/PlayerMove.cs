using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public GameManager gameManager;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    void Update() //단발적인 호출을 위해 사용
    {
        //Jump & infinite Jump limit
        if (Input.GetButtonDown("Jump") && animator.GetBool("isJumping") == false)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);

        }

        //미끄러짐 방지
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.7f, rigid.velocity.y);

        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if (Mathf.Abs(rigid.velocity.x) < 0.5)

            animator.SetBool("isWalking", false);

        else
            animator.SetBool("isWalking", true);


    }

    //Max Speed
    void FixedUpdate() //기본적으로 1초에 50회 호출
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        //AddForce : 힘 추가 1초동안 꾹 누르면 힘이 50번 주어짐

        if (rigid.velocity.x > maxSpeed) // right Max Speed
                                         //velocity : 속도
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }

        else if (rigid.velocity.x < maxSpeed * (-1)) // Left Max Speed
                                                     //velocity : 속도

            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //Landing Platform
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D raycast = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (raycast.collider != null)
            {
                animator.SetBool("isJumping", false);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            //Attack
            if ((collision.transform.position.y < transform.position.y) && rigid.velocity.y < 0)
            {
                //Point
                gameManager.stagePoint += 100;
                //Enemy Die
                OnAttack(collision.transform);
            }

            //Damaged
            else
            {
                OnDamaged(collision.transform.position);
            }
        }

        else if (collision.gameObject.tag == "Finish")
        {
            gameManager.NextStage();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //Trigger로 설정
    {
        if (collision.gameObject.tag == "item")
        {
            //Point

            bool isBronze = collision.gameObject.name.Contains("bronze");
            bool isSilver = collision.gameObject.name.Contains("silver");
            bool isGold = collision.gameObject.name.Contains("gold");

            if (isBronze)
                gameManager.stagePoint += 100;
            else if (isSilver)
                gameManager.stagePoint += 200;
            else if (isGold)
                gameManager.stagePoint += 300;

            //DeActive Item
            collision.gameObject.SetActive(false);
        }

    }

    void OnDamaged(Vector2 targetPos)
    {


        //health down
        gameManager.HealthDown();

        //change layer to PlayerDamaged, Immortal Active
        gameObject.layer = 9;

        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        //Add force to player gameObject for bouncing back from enemy
        int force = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(force, 1) * 8, ForceMode2D.Impulse);

        //animation transition
        animator.SetTrigger("onDamaged");

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        //change layer to Player, Mortal Active
        gameObject.layer = 8;

        //clear transparent Mode
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }

    void OnAttack(Transform enemy)
    {
        //Point

        //EnemyAttack
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
    }

    public void OnDie()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //FlipY
        spriteRenderer.flipY = true;

        //collider unenable
        boxCollider.enabled = false;

        Debug.Log("게임오버");

    }
    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

}