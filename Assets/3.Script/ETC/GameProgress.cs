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

    //countDown
    float timeRemaining;


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

    public void KillPlayer()
    {
        GameManager.instance.KillPlayer();
    }
    
    public void ActiveKillButton(bool isActive)
    {
        killBtn.interactable = isActive;
    }
}
