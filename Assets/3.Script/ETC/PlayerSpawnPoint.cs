using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            GameManager.instance.players[i].transform.position = transform.GetChild(i).position;
        }
    }
}
