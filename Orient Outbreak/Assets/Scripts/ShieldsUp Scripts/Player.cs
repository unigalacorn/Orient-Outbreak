using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    bool isJumping;

    [SerializeField] private ShieldsUpManager gameManager;
    [SerializeField] private Text healthDisplay;

    //player properties
    public int health;

    public List<string> inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<string>();
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && isJumping == false)
        {
            rb.velocity = new Vector3(0, 20, 0);
            isJumping = true;
        }

        if (health <= 0)
        {
            gameManager.GameOver();
        }
    }

    //when player collides with the ground, set isJumping = false so that the player can jump again
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            string itemType = collision.gameObject.GetComponent<Collectable>().itemType;
            print("we have collected a: " + itemType);

            inventory.Add(itemType);
            print("Inventory length: " + inventory.Count);

            Destroy(collision.gameObject);
        }
    }
}
