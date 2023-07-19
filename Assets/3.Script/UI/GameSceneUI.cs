using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [Header("Role text")]
    [SerializeField] Text roleUI;

    [Header("Buttons")]
    [SerializeField] Button reportBtn;
    [SerializeField] Button useBtn;
    [SerializeField] Button killBtn;

    [Header("Task bar")]
    [SerializeField] Slider taskProgressBar;

    [Header("Victory")]
    [SerializeField] Image victory;
    [SerializeField] Text victoryRole;

    [Header("Imposter sight")]
    [SerializeField] Image darkSight;

    private void Start()
    {
        GameProgress.instance.roleUI = this.roleUI;
        GameProgress.instance.reportBtn = this.reportBtn;
        GameProgress.instance.useBtn = this.useBtn;
        GameProgress.instance.killBtn = this.killBtn;
        GameProgress.instance.taskProgressBar = this.taskProgressBar;
        GameProgress.instance.victory = this.victory;
        GameProgress.instance.victoryRole = this.victoryRole;
        GameProgress.instance.darkSight = this.darkSight;
    }
}
