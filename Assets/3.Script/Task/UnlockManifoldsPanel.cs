using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManifoldsPanel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        RandomChangeBtn();
    }

    void RandomChangeBtn()
    {
        int time = 0;
        while (time < 100)
        {
            int a = Random.Range(0, 10);

            transform.GetChild(a).SetAsLastSibling();
            time++;
        }
    }
}
