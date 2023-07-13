using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerColor : MonoBehaviour
{
    [Header("Player Body Materials")]
    [SerializeField] Material[] materials;

    // Start is called before the first frame update
    void Start()
    {
        ChangeBodyColor(GetComponent<PlayerControl>().playerNum);
    }


    void ChangeBodyColor(int playerNum)
    {
        transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = materials[playerNum];
        transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = materials[playerNum];
        transform.GetChild(0).GetChild(2).GetComponent<MeshRenderer>().material = materials[playerNum];
        transform.GetChild(0).GetChild(3).GetComponent<MeshRenderer>().material = materials[playerNum];
    }
}
