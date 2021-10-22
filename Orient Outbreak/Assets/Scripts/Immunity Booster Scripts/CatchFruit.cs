using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFruit : MonoBehaviour
{
    [SerializeField] private Animator characterHolder;
    [SerializeField] private ImmunityBoosterManager immunityBoosterManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            characterHolder.SetTrigger("stopSquash");
            characterHolder.SetTrigger("squashPlayer");
            Destroy(collision.gameObject);

            immunityBoosterManager.catchFruit();
        }

        if (collision.CompareTag("Shake Fruit"))
        {
            immunityBoosterManager.CameraShake();

            characterHolder.SetTrigger("stopSquash");
            characterHolder.SetTrigger("squashPlayer");
            Destroy(collision.gameObject);

            immunityBoosterManager.catchFruit();
        }
    }
}
