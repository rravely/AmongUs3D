using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public void KillPlayerButton()
    {
        GameManager.instance.KillPlayer();
    }
}
