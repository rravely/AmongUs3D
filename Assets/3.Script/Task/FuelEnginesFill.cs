using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelEnginesFill : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] Slider fillbase;
    [SerializeField] Slider gasCanBase;

    [Header("Light")]
    [SerializeField] Image redLight;
    [SerializeField] Image greenLight;

    [Header("Gas Can")]
    [SerializeField] FuelEnginesGasCan fuelEnginesGasCan;

    [Header("UI")]
    [SerializeField] TaskCompleted completed;

    public bool isSuccess;

    //Color
    Color red = new Vector4(1, 0, 0, 1);
    Color redDark = new Vector4(0.5f, 0, 0, 1);
    Color green = new Vector4(0, 1, 0, 1);
    Color greenDark = new Vector4(0, 0.5f, 0, 1);

    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameManager.instance.taskSuccess[1])
        {
            gasCanBase.value = 1f;
            fillbase.value = 0f;
        }
        else
        {
            gasCanBase.value = 0f;
            fillbase.value = 0f;
        }
    }

    public void FillBase()
    {
        if (gasCanBase.value.Equals(1))
        {
            StartCoroutine(FillBase_co());
        }
    }

    IEnumerator FillBase_co()
    {
        redLight.color = red;
        while (gasCanBase.value > 0)
        {
            gasCanBase.value -= 0.001f;
            fillbase.value += 0.001f;
            yield return null;
        }

        redLight.color = redDark;
        greenLight.color = green;

        isSuccess = true;
        GameManager.instance.taskSuccess[2] = true;

        yield return new WaitForSeconds(1f);
        greenLight.color = greenDark;

        completed.gameObject.SetActive(true);
        completed.Completed();
    }
    
}
