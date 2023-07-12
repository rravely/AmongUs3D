using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerCameraSetting : MonoBehaviourPun
{
    [SerializeField] Transform playerCameraRoot;

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine) return;

        playerCameraRoot = transform.GetChild(1);
        if (SceneManager.GetActiveScene().name.Equals("1.GameScene"))
        {
            FindObjectOfType<CameraSetting>().SetTransform(transform);
        }
    }
}
