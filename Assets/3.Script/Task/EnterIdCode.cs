using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterIdCode : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Button[] btns;
    [SerializeField] Text inputCode;
    [SerializeField] Image card;
    [SerializeField] Text randomCode;
    [SerializeField] TaskCompleted completed;

    //My playerControl
    GameProgress gameProgress;

    //Result
    public bool isSuccess = false;

    void Start()
    {
        gameProgress = FindObjectOfType<GameProgress>();
        inputCode.text = "";
    }

    public void PressNumber(string n)
    {
        if (inputCode.text.Length < 8)
        {
            inputCode.text = inputCode.text + n;
        }
    }

    public void PressCancel()
    {
        inputCode.text = "";
    }

    public void PressComfirm()
    {
        if (randomCode.text.Equals(inputCode.text))
        {
            //gameObject.SetActive(false);
            isSuccess = true;
            GameManager.instance.taskSuccess[0] = true;
            GameProgress.instance.MyPlayerTaskSuccess(0);

            completed.gameObject.SetActive(true);
            completed.Completed();
        }
        else
        {

        }
    }
}
