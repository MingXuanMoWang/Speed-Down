using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;

    public float speed;
    float xVelocity;

    public float checkRadius;
    public LayerMask playform;
    public GameObject groundCheck;
    public bool isOnGround;

    bool playerDead;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, checkRadius, playform);

        animator.SetBool("isOnGround", isOnGround);
        Movement();
    }

    void Movement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (xVelocity * speed, rb.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));


        if(xVelocity != 0 ) {
            transform.localScale = new Vector3(xVelocity, 1, 1);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Fan"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spike"))
        {
            animator.SetTrigger("dead");
        }
    }

    public void PlayerDead()
    {
        playerDead = true;
        GameManager.GameOver(playerDead);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.transform.position, checkRadius);
    }
}
