using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SetupCamera : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
