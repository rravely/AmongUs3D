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
    [SerializeField] Text roleUI;
    [SerializeField] Button reportBtn;
    [SerializeField] Button useBtn;
    [SerializeField] Button killBtn;
    [SerializeField] Slider taskProgressBar;

    [SerializeField] Image killedImgae;

    //countDown
    float timeRemaining;

    //task
    public GameObject task;
    public Vent vent;

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
                roleNum = myPlayerControl.playerRole;
                break;
            }
        }

        //view player role
        StartCoroutine(ViewPlayerRole_co());
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

    void SetRoleUI()
    {
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

    public void KillPlayer()
    {
        GameManager.instance.KillPlayer();
    }
    
    public void ActiveKillButton(bool isActive)
    {
        killBtn.interactable = isActive;
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
        myPlayerControl.ChangeTaskSuccess(n);
        TaskProgressBar();
    }

    public void TaskProgressBar()
    {
        int taskProgress = 0;
        for (int i = 0; i < GameManager.instance.taskSuccess.Length; i++)
        {
            if (GameManager.instance.taskSuccess[i].Equals(true))
            {
                taskProgress++;
            }
        }

        taskProgressBar.value = taskProgress / GameManager.instance.taskSuccess.Length;
    }
}
