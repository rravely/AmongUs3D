using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocation : MonoBehaviour
{
    [SerializeField] Text location;
    [SerializeField] string locationName;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerControl>().PV.IsMine)
            {
                location.text = locationName;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerControl>().PV.IsMine)
            {
                location.text = "";
            }
        }
    }
}
