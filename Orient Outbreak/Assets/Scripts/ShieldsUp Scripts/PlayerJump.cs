using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isJumping;

    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space") && !isJumping)
        {
            rb.velocity = new Vector3(0, 20, 0);
            isJumping = true;
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
    }

}
