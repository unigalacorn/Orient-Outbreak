using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissFruit : MonoBehaviour
{
    [SerializeField] private ImmunityBoosterManager gameManagerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            gameManagerScript.missFruit();
        }
    }
}
