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
            SQLManager.instance.DisconnectSQL();

            gameObject.SetActive(false);
            PlayerPrefs.SetString("Name", id.text);
            FindObjectOfType<PunManager>().Connect();
        }
        else
        {
            log.text = "Check id and password.";
        }
    }

    public void JoinCheck()
    {
        if (id.text.Equals(string.Empty) || pwd.text.Equals(string.Empty))
        {
            log.text = "Enter id and password.";
        }
        else
        {
            if(SQLManager.instance.Join(id.text, pwd.text))
            {
                log.text = "Join completely";
            }
            else
            {
                log.text = "Fail to join";
            }
        }
    }
}
