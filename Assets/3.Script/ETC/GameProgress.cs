using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour
{
    //Current player info
    public int roleNum;
    public GameObject myPlayer;
    public PlayerControl myPlayerControl;

    [Header("UI")]
    public Text roleUI;
    public Button reportBtn;
    public Button useBtn;
    public Button killBtn;
    public Slider taskProgressBar;
    public Image victory;
    public Text victoryRole;

    //[SerializeField] Image killedImgae;

    //countDown
    float timeRemaining;

    //task
    public GameObject[] tasks;
    public GameObject task;
    public Vent vent;
    public int taskProgress;

    public static GameProgress instance = null;

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
    }


    void Start()
    {
        //Find my player object
        for (int i = 0; i < GameManager.instance.players.Length; i++)
        {
            if (GameManager.instance.players[i].GetComponent<PlayerControl>().PV.IsMine)
            {
                myPlayer = GameManager.instance.players[i];
                myPlayerControl = myPlayer.GetComponent<PlayerControl>();
                break;
            }
        }
    }

    IEnumerator ViewPlayerRole_co()
    {
        roleUI.gameObject.SetActive(true);
        SetRoleUI();

        float time = 0;
        while (time < 4f)
        {
            yield return null;
        }

        roleUI.gameObject.SetActive(false);

        StartCoroutine(CountDown_co(40f));
    }

    public void SetRoleUI()
    {
        roleUI.gameObject.SetActive(true);

        switch (roleNum)
        {
            case 0:
                roleUI.text = "Crew";
                killBtn.gameObject.SetActive(false);
                break;
            case 1:
                roleUI.text = "Imposter";
                killBtn.gameObject.SetActive(true);
                break;
        }
    }

    IEnumerator CountDown_co(float waitingTime)
    {
        float initTime = Time.time;
        timeRemaining = waitingTime;
        while (timeRemaining > 0)
        {
            timeRemaining = waitingTime - (Time.time - initTime);
            yield return null;
        }
    }

    public void Use()
    {
        if (task != null)
        {
            task.SetActive(true);
        }
        
        if (vent != null)
        {
            vent.MoveByVent();
        }
    }
    
    public void ActiveKillButton(bool isActive)
    {
        if (killBtn != null)
        {
            killBtn.interactable = isActive;
        }
    }

    public void InteractiveButton(int n)
    {
        switch (n)
        {
            case 0:
                reportBtn.interactable = true;
                break;
            case 1:
                useBtn.interactable = true;
                break;
            case 2:
                killBtn.interactable = true;
                break;
        }
    }

    public void InteractiveButtonFalse(int n)
    {
        switch (n)
        {
            case 0:
                reportBtn.interactable = false;
                break;
            case 1:
                useBtn.interactable = false;
                break;
            case 2:
                killBtn.interactable = false;
                break;
        }
    }

    public void MyPlayerTaskSuccess(int n)
    {
        GameManager.instance.SendTaskSuccess(n);
    }

    public void TaskProgressBar()
    {
        taskProgress = 0;
        for (int i = 0; i < GameManager.instance.taskSuccess.Length; i++)
        {
            if (GameManager.instance.taskSuccess[i].Equals(true))
            {
                taskProgress++;
            }
        }

        taskProgressBar.value = (float)taskProgress / (float)GameManager.instance.taskSuccess.Length;
    }

    public void GameOver(int n)
    {
        StartCoroutine(GameOver_co(n));
    }

    IEnumerator GameOver_co(int n)
    {
        //Display UI
        victory.gameObject.SetActive(true);
        
        switch (n)
        {
            case 0:
                victoryRole.text = "Crew";
                break;
            case 1:
                victoryRole.text = "Imposter";
                break;
        }


        float time = 0;
        while (time < 5f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        victory.gameObject.SetActive(false);
        GameManager.instance.ReStartGame();
    }

    public void InactiveTaskUI(bool isActive)
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i].SetActive(isActive);
        }
    }
}
