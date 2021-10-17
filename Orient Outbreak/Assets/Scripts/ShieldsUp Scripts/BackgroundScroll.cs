using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float start;
    [SerializeField] private float end;


    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.fixedDeltaTime, transform.position.y);
        //if the object reaches the end, it will be repositioned again at the start
        if (transform.position.x <= end)
        {
            if (gameObject.tag == "Obstacle")
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = new Vector2(start, transform.position.y);
            }
        }
    }
}
