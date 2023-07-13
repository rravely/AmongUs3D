using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerCameraSetting : MonoBehaviourPun
{
    void Start()
    {
        if (!photonView.IsMine) return;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (SceneManager.GetActiveScene().name.Equals("1.GameScene"))
            {
                FindObjectOfType<CameraSetting>().SetTransform(transform);
            }
        }
    }
}
