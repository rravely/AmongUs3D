using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour
{
    //Current player info
    [Header("Local player")]
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
    public Image darkSight;

    [SerializeField] Sprite ventSprite;
    [SerializeField] Sprite useSprite;

    [Header("Sound")]
    [SerializeField] AudioClip enterMiniGame;
    [SerializeField] AudioClip crewWin;
    [SerializeField] AudioClip imposterWin;

    //[SerializeField] Image killedImgae;

    //countDown
    float timeRemaining;

    //task
    public GameObject[] tasks;
    public GameObject task;
    public Vent vent;
    public int taskProgress;

    //Component
    AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
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
                darkSight.gameObject.SetActive(false);
                useBtn.GetComponent<Image>().sprite = useSprite;
                break;
            case 1:
                roleUI.text = "Imposter";
                killBtn.gameObject.SetActive(true);
                darkSight.gameObject.SetActive(true);
                useBtn.GetComponent<Image>().sprite = ventSprite;
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
        if (roleNum.Equals(0) && task != null)
        {
            audioSource.PlayOneShot(enterMiniGame);
            task.SetActive(true);
        }
        
        if (roleNum.Equals(1) && vent != null)
        {
            myPlayerControl.VentSound();
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
                audioSource.PlayOneShot(crewWin);
                break;
            case 1:
                victoryRole.text = "Imposter";
                audioSource.PlayOneShot(imposterWin);
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
