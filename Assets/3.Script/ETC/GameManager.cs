using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    //Player
    public int maxPlayerNum = 2;
    public GameObject[] players;

    //Player role info
    public int[] playerRole;
    public bool[] playerDead;

    //Spawn point
    [SerializeField] Transform spawnPoint;

    //Change scene
    bool isChange = false;

    //Game start
    public bool isGameStart = false;

    //countDown
    float waitingTime = 3f;
    float timeRemaining;
    [SerializeField] Text countDown;

    //Kill Player
    public int killPlayerNum = 8;
    public int killedCount = 0;

    //Check task success
    public bool[] taskSuccess = new bool[5];

    //Photon View
    PhotonView PV;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        players = new GameObject[maxPlayerNum];
        playerRole = new int[maxPlayerNum];
        playerDead = new bool[maxPlayerNum];

        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length.Equals(maxPlayerNum) && SceneManager.GetActiveScene().name.Equals("0.WaitingRoom") && !isChange)
        {
            isChange = true;
            ChangeGameScene();
        }
    }

    public void ChangeGameScene()
    {
        StartCoroutine(StartCountDown_co());
    }

    IEnumerator StartCountDown_co()
    {
        countDown.gameObject.SetActive(true);

        float initTime = Time.time;
        timeRemaining = waitingTime;
        while (timeRemaining > 0)
        {
            if (timeRemaining < 1f)
            {
                SetPlayerIdle();
            }

            timeRemaining = waitingTime - (Time.time - initTime);
            countDown.text = Mathf.CeilToInt(timeRemaining).ToString();
            yield return null;
        }

        SceneManager.LoadScene("1.GameScene");
        //SetPlayerRole();
    }

    void SetPlayerIdle()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerControl>().PlayerStand();
        }
    }

    public void SetPlayerRole()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int n = Random.Range(0, maxPlayerNum);
            Debug.Log($"Imposter: {n}");

            for (int i = 0; i < playerRole.Length; i++)
            {
                if (i.Equals(n))
                {
                    playerRole[i] = 1;
                }
                else
                {
                    playerRole[i] = 0;
                }
                players[i].GetComponent<PlayerControl>().playerRole = playerRole[i];
            }
            ChangePlayerRole();
        }

        //Change UI
        GameProgress.instance.TaskProgressBar() ;
    }

    public void KillPlayer()
    {
        if (killPlayerNum < 8)
        {
            playerDead[killPlayerNum] = true;
            killedCount++;
            ChangePlayerDeadState();

            killPlayerNum = 8;
            GameProgress.instance.myPlayerControl.canKill = false;
        }
    }

    public void ChangePlayerDeadState()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerControl>().ChangeIsDead();
        }

        //Display player killed
    }

    public void ChangePlayerRole()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerControl>().ChangeRole();
        }
    }


    //Task progress
    [PunRPC]
    public void TaskSuccess(int n)
    {
        taskSuccess[n] = true;
        GameProgress.instance.TaskProgressBar();
    }

    public void SendTaskSuccess(int n)
    {
        PV.RPC("TaskSuccess", RpcTarget.All, n);
    }


    //Start game signal
    [PunRPC]
    void GameStart(bool isStart)
    {
        isGameStart = isStart;
    }

    public void SendStartGame(bool isStart)
    {
        PV.RPC("GameStart", RpcTarget.All, isStart);
    }    

    public void ReStartGame()
    {
        //Reset 
        for (int i = 0; i < playerDead.Length; i++)
        {
            //Reset player dead state
            playerDead[i] = false;
            players[i].GetComponent<PlayerControl>().ChangeIsDead();
        }

        killedCount = 0;

        //Reset task
        for (int i = 0; i < taskSuccess.Length; i++)
        {
            taskSuccess[i] = false;
        }

        GameProgress.instance.TaskProgressBar();

        SceneManager.LoadScene("1.GameScene");
    }

}
