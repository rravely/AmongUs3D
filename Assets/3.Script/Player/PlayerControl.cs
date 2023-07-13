using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerControl : MonoBehaviour
{
    //Player info
    public int playerNum;
    public string playerName;

    //player role
    public bool isViewRole;
    public int playerRole;

    //Player interaction
    public bool canReport = false;
    public bool canUse = false;
    public bool canKill = false;

    //player state
    public bool isDead = false;

    //PhotonView
    public PhotonView PV;

    Animator playerAni;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        playerAni = GetComponent<Animator>();

        if (PV.IsMine)
        {
            PV.RPC("SetPlayerNum", RpcTarget.All, (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerNum"]);
            //PV.RPC("SetPlayerName", RpcTarget.All, PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"].ToString());
        }

        GameManager.instance.players[playerNum] = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PV.IsMine)
            {
                PlayerControl otherPlayerControl;
                other.TryGetComponent(out otherPlayerControl);

                if (!otherPlayerControl.PV.IsMine && !otherPlayerControl.isDead)
                {
                    canKill = true;
                    GameManager.instance.killPlayerNum = otherPlayerControl.playerNum;
                    FindObjectOfType<GameProgress>().ActiveKillButton(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PV.IsMine)
            {
                PlayerControl otherPlayerControl;
                other.TryGetComponent(out otherPlayerControl);

                if (!otherPlayerControl.PV.IsMine && !otherPlayerControl.isDead)
                {
                    canKill = false;
                    GameManager.instance.killPlayerNum = 8;
                    FindObjectOfType<GameProgress>().ActiveKillButton(false);
                }
            }
        }
    }


    [PunRPC]
    void SetPlayerNum(int num)
    {
        playerNum = num;
    }

    [PunRPC]
    void SetPlayerName(string name)
    {
        playerName = name;
    }

    [PunRPC]
    void PlayerDead(bool isDead)
    {
        this.isDead = isDead;
        transform.GetChild(0).gameObject.SetActive(!isDead);
        transform.GetChild(1).gameObject.SetActive(isDead);
    }

    public void ChangeIsDead()
    {
        PV.RPC("PlayerDead", RpcTarget.All, GameManager.instance.playerDead[playerNum]);
    }

}
