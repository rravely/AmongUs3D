using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ChangeGameScene()
    {
        StartCoroutine(CountDown_co());
    }

    IEnumerator CountDown_co()
    {
        float time = 0f;
        while (time < 10f)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            yield return null;
        }

        SceneManager.LoadScene("1.GameScene");
    }
}
