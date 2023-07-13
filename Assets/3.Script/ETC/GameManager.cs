using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    //Player
    int maxPlayerNum = 1;
    public GameObject[] players;

    //countDown
    float time = 5f;
    float timeRemaining;

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
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length.Equals(maxPlayerNum) && SceneManager.GetActiveScene().name.Equals("0.WaitingRoom"))
        {
            ChangeGameScene();
        }
    }

    public void ChangeGameScene()
    {
        //StartCoroutine(CountDown_co());
        SceneManager.LoadScene("1.GameScene");
    }

    IEnumerator CountDown_co()
    {
        timeRemaining = time;
        while (timeRemaining < time)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("1.GameScene");
    }
}
