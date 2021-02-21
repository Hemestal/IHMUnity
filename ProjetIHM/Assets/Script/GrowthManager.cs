using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    private float time;

    void Start()
    {
       time = 0;
    }


    void Update()
    {
        time += Time.deltaTime;
        if(Time.deltaTime > 1)
        {
            Debug.Log("salut ca marche");
            time = 0;
            if (transform.name.StartsWith("Cabbage") && transform.localScale.x < 1)
            {
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
            else if(transform.name.StartsWith("Tomato") && transform.localScale.x < 1)
            {
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }
}
