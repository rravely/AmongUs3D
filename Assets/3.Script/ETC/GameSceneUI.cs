using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text roleUI;
    [SerializeField] Button reportBtn;
    [SerializeField] Button useBtn;
    [SerializeField] Button killBtn;
    [SerializeField] Slider taskProgressBar;
    [SerializeField] Image victory;
    [SerializeField] Text victoryRole;

    private void Start()
    {
        GameProgress.instance.roleUI = this.roleUI;
        GameProgress.instance.reportBtn = this.reportBtn;
        GameProgress.instance.useBtn = this.useBtn;
        GameProgress.instance.killBtn = this.killBtn;
        GameProgress.instance.taskProgressBar = this.taskProgressBar;
        GameProgress.instance.victory = this.victory;
        GameProgress.instance.victoryRole = this.victoryRole;
    }
}
