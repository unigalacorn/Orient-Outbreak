using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isJumping;

    [SerializeField] private Animator animator;

    [SerializeField] private float jumpBufferLength = 0.1f;
    private float jumpBufferCount;

    [SerializeField] private float moveSpeed;

    [SerializeField] private ParticleSystem dustEffect;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    private void Update()
    {
        //manage jump buffer
        if (Input.GetKeyDown("space"))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        //jump in the air
        if (jumpBufferCount >= 0 && !isJumping)
        {
            rb.velocity = new Vector3(0, 20, 0);
            isJumping = true;
            animator.SetBool("isJump", true);
            jumpBufferCount = 0;
            CreateDust();
        }

        if (Input.GetKeyUp("space") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    //when player collides with the ground, set isJumping = false so that the player can jump again
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        animator.SetBool("isJump",false);
    }

    void CreateDust()
    {
        dustEffect.Play();
    }

}
