using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginControl : MonoBehaviour
{
    [SerializeField] InputField id;
    [SerializeField] InputField pwd;

    [SerializeField] Text log;

    public void LoginClick()
    {
        if (id.text.Equals(string.Empty) || pwd.text.Equals(string.Empty))
        {
            log.text = "Enter id and password.";
        }

        if (SQLManager.instance.Login(id.text, pwd.text))
        {
            gameObject.SetActive(false);
            FindObjectOfType<PunManager>().Connect();
        }
        else
        {
            log.text = "Check id and password.";
        }
    }
}
