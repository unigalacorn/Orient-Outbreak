using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour
{
    [SerializeField] private GameObject level;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y);
    }
}
