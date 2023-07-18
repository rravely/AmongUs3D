using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTask : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameProgress.instance.taskProgress.Equals(GameManager.instance.taskSuccess.Length))
        {
            //Restart Game
            GameProgress.instance.GameOver(0);
        }
    }
}
