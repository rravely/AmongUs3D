using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class admin_Card : MonoBehaviour, IDragHandler, IEndDragHandler
{
    Vector3 originalPos;
    string randomCode;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;

        CreateRandomCode();
        GetComponentInChildren<Text>().text = randomCode;
    }

    
    void CreateRandomCode()
    {
        randomCode = "";
        while (randomCode.Length < 6)
        {
            int a = Random.Range(0, 10);
            randomCode += a.ToString();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        //transform.position = Mouse.current.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originalPos;
    }
}
