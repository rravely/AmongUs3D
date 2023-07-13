using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    public int playerNum;
    public string playerName;

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            PV.RPC("SetPlayerNum", RpcTarget.All, (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerNum"]);
            //PV.RPC("SetPlayerName", RpcTarget.All, PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"].ToString());
        }
    }

    [PunRPC]
    void SetPlayerNum(int num)
    {
        playerNum = num;
    }

    void SetPlayerName(string name)
    {
        playerName = name;
    }


}
