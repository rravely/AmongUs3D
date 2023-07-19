using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] Transform targetVent;
    [SerializeField] Transform pivot;
    [SerializeField] Transform jumpPos;
    [SerializeField] Transform insidePos;

    public void EnterVent()
    {
        //Debug.Log("Enter vent");
        //StartCoroutine(OpenVent_co());
    }

    public void ExitVent()
    {

    }

    public void MoveByVent()
    {
        GameProgress.instance.myPlayer.transform.position = new Vector3(targetVent.position.x, 1f, targetVent.position.z);
        GameProgress.instance.myPlayer.transform.rotation = targetVent.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerControl>().PV.IsMine && other.GetComponent<PlayerControl>().playerRole.Equals(1))
        {
            GameProgress.instance.InteractiveButton(1); //Use button
            GameProgress.instance.vent = this; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerControl>().PV.IsMine && other.GetComponent<PlayerControl>().playerRole.Equals(1))
        {
            GameProgress.instance.InteractiveButtonFalse(1);
            GameProgress.instance.vent = null;
        }
    }

    IEnumerator OpenVent_co()
    {
        GameProgress.instance.myPlayer.transform.position = new Vector3(jumpPos.position.x, GameProgress.instance.myPlayer.transform.position.y, jumpPos.position.z);
        while (transform.eulerAngles.x < 360)
        {
            transform.RotateAround(pivot.position, Vector3.right, 200f * Time.deltaTime);
            GameProgress.instance.myPlayer.transform.position += new Vector3(0, 0.01f, 0f);
            Debug.Log(transform.eulerAngles.x);
            yield return null;
        }


    }
}
