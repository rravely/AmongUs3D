using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    //Player
    int maxPlayerNum = 1;
    public GameObject[] players;

    //Player role info
    public int[] playerRole;
    public bool[] playerDead;

    //Spawn point
    [SerializeField] Transform spawnPoint;

    //Change scene
    bool isChange = false;

    //countDown
    float waitingTime = 1f;
    float timeRemaining;
    [SerializeField] Text countDown;

    //Kill Player
    public int killPlayerNum = 8;

    //Check task success
    public bool[] taskSuccess = new bool[4];


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
        playerDead = new bool[maxPlayerNum];
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
            timeRemaining = waitingTime - (Time.time - initTime);
            countDown.text = Mathf.CeilToInt(timeRemaining).ToString();
            yield return null;
        }
        SetPlayerRole();
        SceneManager.LoadScene("1.GameScene");
    }

    public void SetPlayerRole()
    {
        /*
        int a = maxPlayerNum, b = maxPlayerNum;
        a = Random.Range(0, maxPlayerNum);

        while (a.Equals(b) || b.Equals(maxPlayerNum))
        {
            b = Random.Range(0, maxPlayerNum);
        }
        */
        int a = 0, b = 1;

        playerRole = new int[maxPlayerNum];

        for (int i = 0; i < playerRole.Length; i++)
        {
            if (i.Equals(a) || i.Equals(b))
            {
                playerRole[i] = 1; //imposter
            }
            else
            {
                playerRole[i] = 0;
            }

            //playerRole[1] = 0;
            playerRole[0] = 1;

            players[i].GetComponent<PlayerControl>().playerRole = playerRole[i];
        }
        
    }

    public void KillPlayer()
    {
        if (killPlayerNum < 8)
        {
            playerDead[killPlayerNum] = true;
            ChangePlayerDeadState();
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

}
