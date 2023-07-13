using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetting : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCam;

    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetTransform(Transform t)
    {
        virtualCam.Follow = t;
        virtualCam.LookAt = t;
    }
}
