using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI TomatoSeedNumber, CabbageSeedNumber;
    public GameObject image;
    public static int TomatoSeedCounter, CabbageSeedCounter;
    public static int Coins;
    public float time;

    void Start()
    {
        TomatoSeedCounter = CabbageSeedCounter = 0;
        time = 0.0f;
        image.GetComponent<Image>().color = new Color(100.0f, 60.0f, 60.0f);
    }


    void Update()
    {
        /*
        time += Time.deltaTime;
        if (time > 2)
        {
            TomatoSeedCounter++;
            time++;
            TomatoSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = TomatoSeedCounter.ToString();

            if (time > 4)
            {
                CabbageSeedCounter++;
                CabbageSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = CabbageSeedCounter.ToString();
                time = 0.0f;
            }
        }*/
        TomatoSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = TomatoSeedCounter.ToString();
        CabbageSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = CabbageSeedCounter.ToString();
    }
    public void addSeedTomato()
    {
        TomatoSeedCounter++;
        time++;
        TomatoSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = TomatoSeedCounter.ToString();
    }
    public void addSeedCabbage()
    {
        CabbageSeedCounter++;
        CabbageSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = CabbageSeedCounter.ToString();
        time = 0.0f;
    }
}
