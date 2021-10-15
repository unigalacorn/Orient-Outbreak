using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public int damage;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //player takes damage
            other.GetComponent<Player>().health -= damage;

            Debug.Log(other.GetComponent<Player>().health);

            Destroy(gameObject); //destroy obstacle when in contact with player
        }
    }
}
