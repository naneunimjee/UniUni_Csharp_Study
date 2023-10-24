using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        Invoke("Think", 2);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //prevent falling
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y - 1);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    public void Think()
    {
        //랜덤클래스를 통해 이동방향 설정
        nextMove = Random.Range(-1, 2);

        //애니메이션 전환
        animator.SetInteger("Speed", nextMove);

        //방향 전환
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;

        //재귀 딜레이 랜덤 설정
        float nextThinkTime = Random.Range(2f, 5f);

        //Recursive
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        CancelInvoke();
        Invoke("Think", 2);
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
    }

    public void OnDamaged()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //FlipY
        spriteRenderer.flipY = true;

        //collider unenable
        boxCollider.enabled = false;

        //Deactive delay
        Invoke("DeActive", 3);
    }

    public void DeActive()
    {
        gameObject.SetActive(false);
    }
}