using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;


    private void Update()
    {
        transform.position = new Vector3(playerTransform.position.x + 6, transform.position.y, transform.position.z);
    }
}
