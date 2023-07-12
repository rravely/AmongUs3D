using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSetting : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCam;

    // Start is called before the first frame update
    void Start()
    {
        //virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetTransform(Transform t)
    {
        virtualCam.Follow = t;
        virtualCam.LookAt = t;
    }
}
