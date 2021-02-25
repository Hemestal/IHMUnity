using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public float time;

    void Start()
    {
       time = 0;
    }


    void Update()
    {
        time += Time.deltaTime;
        if(time > 3.0f)
        {
            time = 0;
            if (transform.name.StartsWith("Cabbage") && transform.localScale.x < 1.0f)
            {
                transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                if(transform.localScale.x >= 1.0f)
                {
                    gameObject.tag = "Selectable";
                } 
            }
            else if(transform.name.StartsWith("Tomato") && transform.localScale.x < 2.0f)
            {
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                if (transform.localScale.x >= 2.0f)
                {
                    gameObject.tag = "Selectable";
                }
            }
        }
    }
}
