using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellFruit : MonoBehaviour
{
    public static bool SellTomato, SellCabbage = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider player)
    {
        try
        {
            if (player.gameObject.transform.parent.gameObject.name == "Box_01")
            {
                SellTomato = true;
            }
            if (player.gameObject.transform.parent.gameObject.name == "Box_02")
            {
                SellCabbage = true;
            }
        }
        catch
        {

        }
    }
    private void OnTriggerExit(Collider player)
    {
        try
        {
            if (player.gameObject.transform.parent.gameObject.name == "Box_01")
            {
                SellTomato = false;
            }
            if (player.gameObject.transform.parent.gameObject.name == "Box_02")
            {
                SellCabbage = false;
            }
        }
        catch
        {

        }
    }
}
