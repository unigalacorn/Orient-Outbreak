using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField] private float moveSpeed;
    private Rigidbody2D playerRB;
    private Animator playerAnim;

    private Vector2 movement;
    #endregion

    #region Unity Methods
    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(GameManager.instance.currentState == GameState.Exploration)        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement != Vector2.zero)
            {
                playerAnim.SetFloat("Horizontal", movement.x);
                playerAnim.SetFloat("Vertical", movement.y);

                //Play walking sfx
                if (!AudioManager.instance.GetSource("Walk").isPlaying)
                {
                    AudioManager.instance.Play("Walk");
                }
            }
            playerAnim.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.currentState == GameState.Exploration)
        {
            playerRB.MovePosition(playerRB.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
    #endregion
}
