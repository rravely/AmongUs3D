using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillButton : MonoBehaviour
{
    GameProgress gameProgress;

    Button btn;

    // Start is called before the first frame update
    void Start()
    {
        gameProgress = FindObjectOfType<GameProgress>();

        btn = GetComponent<Button>();
    }
}
