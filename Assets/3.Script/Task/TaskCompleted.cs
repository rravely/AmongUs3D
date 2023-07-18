using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskCompleted : MonoBehaviour
{
    RectTransform rect;

    Vector3 originPos = new Vector3(960f, -50f, 0f);

    [SerializeField] GameObject[] tasks;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Completed()
    {
        StartCoroutine(TaskCompleted_co());
    }

    IEnumerator TaskCompleted_co()
    {
        transform.position = originPos;
        while (transform.position.y < 540)
        {
            transform.position += new Vector3(0f, 15f, 0f);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
        SetInActiveTask();
    }

    void SetInActiveTask()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i].SetActive(false);
        }
    }
}
