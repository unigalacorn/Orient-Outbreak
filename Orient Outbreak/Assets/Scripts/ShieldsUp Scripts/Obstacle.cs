using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ShieldsUpManager shieldsUpManager;

    [SerializeField] private GameObject obstacleEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //player takes damage
            shieldsUpManager.DecreasePlayerHealth();

            Instantiate(obstacleEffect, transform.position, Quaternion.identity);

            Destroy(gameObject); //destroy obstacle when in contact with player
        }
    }
}
