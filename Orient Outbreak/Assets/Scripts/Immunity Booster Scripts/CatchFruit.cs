using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFruit : MonoBehaviour
{
    [SerializeField] private Animator characterHolder;
    [SerializeField] private ImmunityBoosterManager gameManagerScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            characterHolder.SetTrigger("stopSquash");
            characterHolder.SetTrigger("squashPlayer");
            Destroy(collision.gameObject);

            gameManagerScript.catchFruit();
        }
    }
}
