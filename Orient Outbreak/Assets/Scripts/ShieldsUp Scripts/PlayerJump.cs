using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isJumping;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    private void Update()
    {
        if (Input.GetKey("space") && isJumping == false)
        {
            rb.velocity = new Vector3(0, 20, 0);
            isJumping = true;
        }
    }

    //when player collides with the ground, set isJumping = false so that the player can jump again
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }

}
