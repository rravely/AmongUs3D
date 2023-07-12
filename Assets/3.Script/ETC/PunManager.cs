using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PunManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";

    [Header("Server Setting")]
    public ServerSettings setting = null;

    [Header("Player Prefabs")]
    public GameObject playerPrefab;

    [Header("Spawn Point")]
    [SerializeField] Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Connect()
    {
        PhotonNetwork.GameVersion = gameVersion; //Version

        //Connect to Master server
        PhotonNetwork.ConnectToMaster(setting.AppSettings.Server, setting.AppSettings.Port, "");
        Debug.Log("Connect to Master server...");
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master server");

        PhotonNetwork.JoinLobby();  //Move to Lobby from master server
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobby");
        base.OnJoinedLobby();

        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log("No empty room");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 8 });
        Debug.Log("Room created");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Join the room");

        //Create player
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);
    }
}
