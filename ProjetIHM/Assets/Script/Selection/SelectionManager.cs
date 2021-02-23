﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    private Transform _selection;
    public GameObject tomato,cabbage, TomatoPlant, CabbagePlant;
    public TextMeshPro TomatoText, CabbageText;
    public static int TomatoCounter, CabbageCounter;
    // Start is called before the first frame update
    void Start()
    {
        TomatoCounter = CabbageCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material.shader = Shader.Find("Diffuse");
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Selectable") || selection.CompareTag("Tomato") || selection.CompareTag("Cabbage") || selection.CompareTag("BuyTomato") || selection.CompareTag("BuyCabage"))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
                    if (Input.GetKeyDown("e"))
                    {
                        if (selection.CompareTag("Tomato")) //tomate à récolter
                        {
                            TomatoCounter++;
                            GameObject box = GameObject.Find("Box_01");
                            selection.gameObject.transform.SetParent(box.transform);
                            selection.gameObject.transform.position = box.transform.position + new Vector3(0.0f, 1f, 0.0f);
                            selection.gameObject.name = "HarvestedTomato" + TomatoCounter;
                            TomatoText.transform.GetComponent<TextMeshPro>().text = TomatoCounter + "\nTomatoes";
                        }

                        else if (selection.CompareTag("Cabbage")) //tomate à récolter
                        {
                            CabbageCounter++;
                            GameObject box = GameObject.Find("Box_02");
                            selection.gameObject.transform.SetParent(box.transform);
                            selection.gameObject.transform.position = box.transform.position + new Vector3(0.0f, 1f, 0.0f);
                            selection.gameObject.name = "HarvestedCabbage" + CabbageCounter;
                            CabbageText.transform.GetComponent<TextMeshPro>().text = CabbageCounter + "\nCabbages";
                        }

                        else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "TomatoPlant") //Recolte plant de tomate
                        {
                            Destroy(selectionRenderer.gameObject);
                            GameObject fruit1 = Instantiate(tomato) as GameObject;
                            fruit1.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 1f, 0f);
                            GameObject fruit2 = Instantiate(tomato) as GameObject;
                            fruit2.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 1.5f, 0f);
                            GameObject fruit3 = Instantiate(tomato) as GameObject;
                            fruit3.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 2f, 0f);

                            //find shoot corresponding to the tomatoPlant
                            string text = selection.gameObject.name;
                            text = text.Substring(12, 2);
                            GameObject shoot = GameObject.Find("TomatoShoot").transform.Find("shoot" + text).gameObject;
                            shoot.tag = "Selectable";
                        }

                        else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "Cabbages") //Recolte choux
                        {
                            Destroy(selectionRenderer.gameObject);
                            GameObject fruit1 = Instantiate(cabbage) as GameObject;
                            fruit1.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 1f, 0f);

                            //find shoot corresponding to the tomatoPlant
                            string text = selection.gameObject.name;
                            text = text.Substring(8, 2);
                            GameObject shoot = GameObject.Find("CabbageShoot").transform.Find("shoot" + text).gameObject;
                            shoot.tag = "Selectable";
                        }

                        else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "CabbageShoot" && InventoryUI.CabbageSeedCounter > 0) //Seme choux
                        {
                            InventoryUI.CabbageSeedCounter--;
                            GameObject Plant = Instantiate(CabbagePlant) as GameObject;
                            Plant.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                            Plant.transform.SetParent(GameObject.Find("Cabbages").transform);
                            Plant.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(0f, 0.05f, 0f);

                            //Rename Tomato(clone)
                            string text = selection.gameObject.name;
                            text = text.Substring(5, 2);
                            Plant.gameObject.name = "Cabbage_" + text;
                            Plant.gameObject.AddComponent<GrowthManager>();

                            selection.gameObject.tag = "Untagged";
                        }

                        else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "TomatoShoot" && InventoryUI.TomatoSeedCounter > 0) //Seme plant de tomate
                        {
                            InventoryUI.TomatoSeedCounter--;
                            GameObject Plant = Instantiate(TomatoPlant) as GameObject;
                            Plant.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                            Plant.transform.SetParent(GameObject.Find("TomatoPlant").transform);
                            Plant.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(0f, 0.05f, 0f);

                            //Rename Tomato(clone)
                            string text = selection.gameObject.name;
                            text = text.Substring(5, 2);
                            Plant.gameObject.name = "TomatoPlant_" + text;
                            Plant.gameObject.AddComponent<GrowthManager>();

                            selection.gameObject.tag = "Untagged";
                        }
                        else if (selection.CompareTag("BuyTomato")) //graine de tomate a acheter
                        {
                            InventoryUI.TomatoSeedCounter++;
                        }
                        else if (selection.CompareTag("BuyCabage")) //graine de tomate a acheter
                        {
                            InventoryUI.CabbageSeedCounter++;
                        }
                    }
                    _selection = selection;
                }
            }
        }
    }
}
