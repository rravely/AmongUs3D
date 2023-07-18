using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeath : MonoBehaviour
{
    bool isLock = false;

    private void Start()
    {
        isLock = false;
    }

    void Update()
    {
        if (CountPlayerKilled().Equals(GameManager.instance.maxPlayerNum - 1) && !isLock)
        {
            isLock = true;
            GameProgress.instance.GameOver(1);
        }
    }

    int CountPlayerKilled()
    {
        int count = 0;
        for (int i = 0; i < GameManager.instance.maxPlayerNum; i++)
        {
            if (GameManager.instance.playerDead[i])
            {
                count++;
            }
        }
        return count;
    }
}
