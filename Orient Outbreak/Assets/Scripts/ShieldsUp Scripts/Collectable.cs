using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private ShieldsUpManager shieldsUpManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //player collects item
            shieldsUpManager.CollectItem();

            Destroy(gameObject); 
        }
    }
}
