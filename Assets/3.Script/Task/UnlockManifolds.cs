using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManifolds : MonoBehaviour
{
    int currentNum;

    [Header("Buttons")]
    [SerializeField] Button[] btns;

    [Header("Task Completed")]
    [SerializeField] TaskCompleted completed;

    public bool isSuccess = false;

    // Start is called before the first frame update
    void Start()
    {
        currentNum = 0;
    }

    public void UnlockManifoldsBtn(int n)
    {
        if (n.Equals(currentNum + 1))
        {
            //correct
            currentNum = n;

            //change color
            btns[n - 1].GetComponent<Image>().color = Color.green;

            if (n.Equals(10))
            {
                //task completed
                completed.gameObject.SetActive(true);
                completed.Completed();

                GameManager.instance.taskSuccess[4] = true;
                GameProgress.instance.MyPlayerTaskSuccess(4);
            }
        }
        else
        {
            //incorrect
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].GetComponent<Image>().color = Color.white;
            }
            currentNum = 0;
        }
    }
}
