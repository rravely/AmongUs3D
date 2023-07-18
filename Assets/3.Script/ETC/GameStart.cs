using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.SetPlayerRole();
    }

    public void ChangeRole()
    {
        GameManager.instance.SetPlayerRole();
    }

    private void Update()
    {
        GameProgress.instance.roleNum = GameProgress.instance.myPlayerControl.playerRole;
        GameProgress.instance.SetRoleUI();
    }
}
