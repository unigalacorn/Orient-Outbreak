using System.Collections;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    #region Variables
    //Facing Direction
    [SerializeField] private SpriteRenderer playerSprite;
    private bool isFacingRight;

    //Button Buffer
    [SerializeField] private float moveBufferDuration;
    private float moveLeftBufferCount;
    private float moveRightBufferCount;

    //Movement
    [SerializeField] private float timeToMove = 0.1f;
    private bool isMoving;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private int gridPosition;
    #endregion

    #region Unity Methods
    private void Start()
    {
        isFacingRight = true;   //Set facing right as default
        gridPosition = 3;       //Set grid position to the middle grid
    }

    private void Update()
    {
        ButtonBuffer();
        MovePlayer();
    }
    #endregion

    #region Private Mehods
    private void ButtonBuffer()
    {
        //Buffer Button
        moveLeftBufferCount = (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ? moveBufferDuration : moveLeftBufferCount -= Time.deltaTime;
        moveRightBufferCount = (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ? moveBufferDuration : moveRightBufferCount -= Time.deltaTime;
    }

    private void MovePlayer()
    {
        //Movement
        if (moveLeftBufferCount >= 0 && !isMoving && gridPosition != 1)         //Move Character to Left Grid
        {
            if (isFacingRight)
            {
                playerSprite.flipX = true;
                isFacingRight = false;
            }

            gridPosition -= 1;
            moveLeftBufferCount = 0;
            StartCoroutine(MovePlayer(new Vector3(-0.5f, 0f, 0f)));
        }

        if (moveRightBufferCount >= 0 && !isMoving && gridPosition != 5)       //Move Character to Right Grid
        {
            if (!isFacingRight)
            {
                playerSprite.flipX = false;
                isFacingRight = true;
            }

            moveRightBufferCount = 0;
            gridPosition += 1;
            StartCoroutine(MovePlayer(new Vector3(0.5f, 0f, 0f)));
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator MovePlayer(Vector3 direction)       //Method that moves player to grid
    {
        isMoving = true;
        float elapsedTime = 0f;
        originalPosition = transform.position;
        targetPosition = originalPosition + direction;

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
    #endregion
}
