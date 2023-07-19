using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomSeat : MonoBehaviour
{
    [SerializeField] Transform seatPos;
    [SerializeField] Transform standPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerControl>().PV.IsMine)
        {
            other.GetComponent<PlayerControl>().isInSeat = true;
            other.GetComponent<PlayerControl>().seatPos = this.seatPos.position;
            other.GetComponent<PlayerControl>().seatRot = seatPos.eulerAngles;
            other.GetComponent<PlayerControl>().standPos = this.standPos.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerControl>().PV.IsMine)
        {
            other.GetComponent<PlayerControl>().isInSeat = false;
        }
    }
}
