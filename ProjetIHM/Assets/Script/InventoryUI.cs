using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI TomatoSeedNumber, CabbageSeedNumber;
    public int TomatoCounter, CabbageCounter;
    public float time;

    void Start()
    {
        TomatoCounter = CabbageCounter = 0;
        time = 0.0f;
    }


    void Update()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            TomatoCounter++;
            time++;
            TomatoSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = TomatoCounter.ToString();

            if (time > 4)
            {
                CabbageCounter++;
                CabbageSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = CabbageCounter.ToString();
                time = 0.0f;
            }
        }
    }
}
