using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameStartButton : MonoBehaviour
{
    Button startBtn;

    private void Start()
    {
        startBtn = GetComponent<Button>();
        startBtn.interactable = false;
    }

    private void Update()
    {
        /*
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            startBtn.interactable = true;
        }
        else if (!PhotonNetwork.IsMasterClient && GameManager.instance.isGameStart)
        {
            GameStart();
        }
        */
    }

    public void GameStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.instance.isGameStart = true;
            GameManager.instance.SendStartGame(true);
        }

        //Set player number
        GameManager.instance.maxPlayerNum = PhotonNetwork.CountOfPlayers;

        //Start game! (Change game scene)
        GameManager.instance.ChangeGameScene();
    }
}
