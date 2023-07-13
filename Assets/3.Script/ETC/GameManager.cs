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

    //Spawn point
    [SerializeField] Transform spawnPoint;

    //Change scene
    bool isChange = false;

    //countDown
    float waitingTime = 5f;
    float timeRemaining;
    [SerializeField] Text countDown;

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
        if (PhotonNetwork.PlayerList.Length.Equals(maxPlayerNum) && SceneManager.GetActiveScene().name.Equals("0.WaitingRoom") && !isChange)
        {
            isChange = true;
            ChangeGameScene();
        }
    }

    public void ChangeGameScene()
    {
        StartCoroutine(CountDown_co());
        //SceneManager.LoadScene("1.GameScene");
    }

    IEnumerator CountDown_co()
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

        SceneManager.LoadScene("1.GameScene");
    }
}
