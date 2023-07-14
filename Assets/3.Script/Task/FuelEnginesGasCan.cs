using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelEnginesGasCan : MonoBehaviour
{
    [Header("Fill Slider")]
    [SerializeField] Slider gasCanFill;
    [Header("Light")]
    [SerializeField] Image redLight;
    [SerializeField] Image greenLight;

    public bool isSuccess;

    //Color
    Color red = new Vector4(1, 0, 0, 1);
    Color redDark = new Vector4(0.5f, 0, 0, 1);
    Color green = new Vector4(0, 1, 0, 1);
    Color greenDark = new Vector4(0, 0.5f, 0, 1);

    void Start()
    {
        gasCanFill.value = 0;

        redLight.color = redDark;
        greenLight.color = greenDark;
    }

    public void FillGasCan()
    {
        StartCoroutine(FillGasCan_co());
    }

    IEnumerator FillGasCan_co()
    {
        redLight.color = red;
        while (gasCanFill.value < 1)
        {
            gasCanFill.value += 0.001f;
            yield return null;
        }

        redLight.color = redDark;
        greenLight.color = green;
        isSuccess = true;
        GameManager.instance.taskSuccess[1] = true;

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
