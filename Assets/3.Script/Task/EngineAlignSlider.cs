using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EngineAlignSlider : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    float maxRotZ = 40f;
    float minRotz = -40f;

    [SerializeField] Transform pivotTransform;
    Vector2 pivot;
    Vector3 rot = new Vector3(0f, 0f, 0f);

    [Header("Task panel")]
    [SerializeField] GameObject task;

    [Header("Engine")]
    [SerializeField] Transform engine;

    [Header("Engine Line")]
    [SerializeField] Image line;

    [Header("UI")]
    [SerializeField] TaskCompleted completed;

    bool isStart = false;
    float alignTime = 0f;
    float angle = -12f;

    void Start()
    {
        pivot = new Vector2(pivotTransform.GetComponent<RectTransform>().rect.x, pivotTransform.GetComponent<RectTransform>().rect.y);

        pivotTransform.eulerAngles = new Vector3(0, 0, angle);
        engine.eulerAngles = new Vector3(0, 0, minRotz);
    }

    void Update()
    {
        if (isStart)
        {
            if (angle < 0.5f)
            {
                alignTime += Time.deltaTime;

                //change color
                engine.GetComponentInChildren<Image>().color = Color.white;
                line.color = Color.green;
            }
            else
            {
                engine.GetComponentInChildren<Image>().color = Color.red;
                line.color = Color.white;
            }

            if (alignTime > 1f)
            {
                completed.gameObject.SetActive(true);
                completed.Completed();
                GameManager.instance.taskSuccess[3] = true;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isStart = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = pivot - new Vector2(Input.mousePosition.x - 960, Input.mousePosition.y - 540);
        angle = Vector2.Angle(Vector2.left, direction);

        if (angle < maxRotZ && angle > minRotz && Input.mousePosition.y <= 540)
        {
            rot = new Vector3(0, 0, angle * 12 / maxRotZ);
            engine.eulerAngles = new Vector3(0, 0, angle);
        }
        else if (angle < maxRotZ && angle > minRotz && Input.mousePosition.y > 540)
        {
            rot = new Vector3(0, 0, -angle * 12 / maxRotZ);
            engine.eulerAngles = new Vector3(0, 0, -angle);
        }
        pivotTransform.eulerAngles = rot;
    }
}
