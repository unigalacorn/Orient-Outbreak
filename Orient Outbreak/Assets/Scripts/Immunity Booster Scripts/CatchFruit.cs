using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFruit : MonoBehaviour
{
    [SerializeField] private Animator characterHolder;
    [SerializeField] private ImmunityBoosterManager immunityBoosterManager;
    [SerializeField] private ParticleSystem fruitBurst;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit"))
        {
            PlayerCatchFruit();
            Destroy(collision.gameObject);
            Instantiate(fruitBurst, collision.gameObject.transform.position, Quaternion.identity);

        }

        if (collision.CompareTag("Shake Fruit"))
        {
            immunityBoosterManager.CameraShake();

            PlayerCatchFruit();
            Destroy(collision.gameObject);
            Instantiate(fruitBurst, collision.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void PlayerCatchFruit()
    {
        characterHolder.SetTrigger("stopSquash");
        characterHolder.SetTrigger("squashPlayer");
        immunityBoosterManager.catchFruit();
    }
}
