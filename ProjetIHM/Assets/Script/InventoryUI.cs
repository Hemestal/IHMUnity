using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI TomatoSeedNumber, CabbageSeedNumber;
    public TextMeshProUGUI CoinsNumber;
    public static int TomatoSeedCounter, CabbageSeedCounter;
    public static int Coins;
    public float time;

    void Start()
    {
        TomatoSeedCounter = CabbageSeedCounter = 0;
        Coins = 10;
        time = 0.0f;
    }


    void Update()
    {
        TomatoSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = TomatoSeedCounter.ToString();
        CabbageSeedNumber.transform.GetComponent<TextMeshProUGUI>().text = CabbageSeedCounter.ToString();
        CoinsNumber.transform.GetComponent<TextMeshProUGUI>().text = Coins.ToString()+" Coins";
    }
}
