using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseTaskObject : MonoBehaviour
{
    bool canUse;

    [SerializeField] GameObject task;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerControl>().PV.IsMine)
        {
            canUse = true;
            GameProgress.instance.task = this.task;
            GameProgress.instance.InteractiveButton(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerControl>().PV.IsMine)
        {
            canUse = false;
            GameProgress.instance.task = null;
            GameProgress.instance.InteractiveButtonFalse(1);
        }
    }
}
