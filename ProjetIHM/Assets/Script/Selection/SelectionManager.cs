﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    private Transform _selection;
    public GameObject tomato,cabbage, TomatoPlant, CabbagePlant, Box1, Box2;
    public TextMeshPro TomatoText, CabbageText;
    public static int TomatoCounter, CabbageCounter;
    private bool pushT = false, pushF = true;
    public static bool carT = false, carF = true;
    public GameObject Box, player, car, buyCar;
    public Camera CamMain, CamCar;

    // Start is called before the first frame update
    void Start()
    {
        TomatoCounter = CabbageCounter = 0;
        CamCar.enabled = false;
        car.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(carF)
        {
            if (_selection != null)
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
                if (selection.CompareTag("Selectable") || selection.CompareTag("Tomato") || selection.CompareTag("Cabbage") || selection.CompareTag("BuyTomato") || selection.CompareTag("BuyCabage") || selection.CompareTag("Sell") || selection.CompareTag("Car") || selection.CompareTag("BuyCar"))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    if (selectionRenderer != null & hit.distance < 3f)
                    {
                        //selectionRenderer.material.shader = Shader.Find("Self-Illumin/Outlined diffuse");
                        selectionRenderer.material.shader = Shader.Find("Ultimate 10+ Shaders/Force Field");
                        if (Input.GetKeyDown("e"))
                        {
                            if (selection.CompareTag("Tomato")) //tomate à récolter
                            {
                                FindObjectOfType<SoundManager>().Play("Recup");
                                TomatoCounter++;
                                GameObject box = GameObject.Find("Box_01");

                                selection.gameObject.transform.SetParent(box.transform);
                                selection.gameObject.transform.position = box.transform.position + new Vector3(0.0f, 1f, 0.0f);
                                selection.gameObject.name = "HarvestedTomato" + TomatoCounter;
                                selection.gameObject.tag = "HarvestedTomato";
                                TomatoText.transform.GetComponent<TextMeshPro>().text = TomatoCounter + "\nTomatoes";
                            }
                            else if (selection.CompareTag("Cabbage")) //Cabbage à récolter
                            {
                                FindObjectOfType<SoundManager>().Play("Recup");
                                CabbageCounter++;
                                GameObject box = GameObject.Find("Box_02");

                                selection.gameObject.transform.SetParent(box.transform);
                                selection.gameObject.transform.position = box.transform.position + new Vector3(0.0f, 1f, 0.0f);
                                selection.gameObject.name = "HarvestedCabbage" + CabbageCounter;
                                selection.gameObject.tag = "HarvestedCabbage";
                                CabbageText.transform.GetComponent<TextMeshPro>().text = CabbageCounter + "\nCabbages";
                            }
                            else if (selection.CompareTag("Car"))
                            {
                                CamMain.enabled = false;
                                CamCar.enabled = true;
                                CamMain.gameObject.SetActive(false);
                                CamCar.gameObject.SetActive(true);
                                carF = false;
                                carT = true;
                                player.SetActive(false);
                            }
                            else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "Cabbages") //Recolte choux
                            {
                                FindObjectOfType<SoundManager>().Play("CabbageRecolte");
                                Destroy(selectionRenderer.gameObject);

                                GameObject fruit1 = Instantiate(cabbage) as GameObject;
                                fruit1.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 1f, 0f);

                                //find shoot corresponding to the tomatoPlant
                                string text = selection.gameObject.name;
                                text = text.Substring(8, 2);
                                GameObject shoot = GameObject.Find("CabbageShoot").transform.Find("shoot" + text).gameObject;
                                shoot.tag = "Selectable";
                            }
                            else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "TomatoPlant") //Recolte plant de tomate
                            {
                                FindObjectOfType<SoundManager>().Play("TomatoRecolte");
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
                            else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "CabbageShoot" && InventoryUI.CabbageSeedCounter > 0) //Seme choux
                            {
                                FindObjectOfType<SoundManager>().Play("Plant");
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
                                FindObjectOfType<SoundManager>().Play("Plant");
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
                            else if (selection.CompareTag("BuyTomato") && InventoryUI.Coins >= 1) //graine de tomate a acheter
                            {
                                FindObjectOfType<SoundManager>().Play("Pay");
                                InventoryUI.TomatoSeedCounter++;
                                InventoryUI.Coins--;
                            }
                            else if (selection.CompareTag("BuyCabage") && InventoryUI.Coins >= 2) //graine de tomate a acheter
                            {
                                FindObjectOfType<SoundManager>().Play("Pay");
                                InventoryUI.CabbageSeedCounter++;
                                InventoryUI.Coins -= 2;
                            }
                            else if (selectionRenderer.gameObject.transform.gameObject.name == "Box_01")
                            {
                                Rigidbody RigidBox = selectionRenderer.gameObject.transform.gameObject.GetComponent<Rigidbody>();
                                if (pushF)
                                {
                                    FindObjectOfType<SoundManager>().Play("WoodSound");
                                    RigidBox.isKinematic = true;
                                    selectionRenderer.gameObject.transform.SetParent(CamMain.transform);
                                    selectionRenderer.gameObject.transform.localPosition = new Vector3(0, 0, 2);
                                    selectionRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                                    Box1.transform.Find("Top").gameObject.SetActive(true);
                                    pushF = false;
                                    pushT = true;
                                }

                                else if (pushT)
                                {
                                    Box1.transform.Find("Top").gameObject.SetActive(false);
                                    selectionRenderer.gameObject.transform.SetParent(Box.transform);
                                    selectionRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                                    RigidBox.isKinematic = false;
                                    pushF = true;
                                    pushT = false;
                                }
                            }
                            else if (selectionRenderer.gameObject.transform.gameObject.name == "Box_02")
                            {
                                Rigidbody RigidBox = selectionRenderer.gameObject.transform.gameObject.GetComponent<Rigidbody>();
                                if (pushF)
                                {
                                    Box2.transform.Find("Top").gameObject.SetActive(true);
                                    FindObjectOfType<SoundManager>().Play("WoodSound");
                                    RigidBox.isKinematic = true;
                                    selectionRenderer.gameObject.transform.SetParent(CamMain.transform);
                                    selectionRenderer.gameObject.transform.localPosition = new Vector3(0, 0, 2);
                                    selectionRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                                    pushF = false;
                                    pushT = true;
                                }
                                else if (pushT)
                                {
                                    Box2.transform.Find("Top").gameObject.SetActive(false);
                                    selectionRenderer.gameObject.transform.SetParent(Box.transform);
                                    selectionRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                                    RigidBox.isKinematic = false;
                                    pushF = true;
                                    pushT = false;
                                }
                            }
                            else if (selection.CompareTag("Sell")) //graine de tomate a acheter
                            {
                                if (SellFruit.SellTomato == true)
                                {
                                    FindObjectOfType<SoundManager>().Play("EarnMoney");
                                    var NbTomato = GameObject.FindGameObjectsWithTag("HarvestedTomato");
                                    InventoryUI.Coins += NbTomato.Length;
                                    foreach (GameObject tomato in NbTomato)
                                    {
                                        GameObject.Destroy(tomato);
                                    }
                                    TomatoText.transform.GetComponent<TextMeshPro>().text = "0\nTomatoes";
                                    TomatoCounter = 0;
                                }
                                if (SellFruit.SellCabbage == true)
                                {
                                    FindObjectOfType<SoundManager>().Play("EarnMoney");
                                    var NbCabbage = GameObject.FindGameObjectsWithTag("HarvestedCabbage");
                                    InventoryUI.Coins += NbCabbage.Length * 3;
                                    foreach (GameObject cabbage in NbCabbage)
                                    {
                                        GameObject.Destroy(cabbage);
                                    }
                                    CabbageText.transform.GetComponent<TextMeshPro>().text = "0\nCabbages";
                                    CabbageCounter = 0;
                                }
                            }
                            else if (selection.CompareTag("BuyCar") && InventoryUI.Coins >= 10) //graine de tomate a acheter
                            {
                                FindObjectOfType<SoundManager>().Play("Easter Egg");
                                car.SetActive(true);
                                InventoryUI.Coins -= 10;
                                buyCar.SetActive(false);
                            }
                        }
                        _selection = selection;
                    }
                }
            }
        }
        else if (carT && Input.GetKeyDown("e"))
        {
            CamMain.enabled = true;
            CamCar.enabled = false;
            CamMain.gameObject.SetActive(true);
            CamCar.gameObject.SetActive(false);
            carF = true;
            carT = false;
            player.SetActive(true);
            player.transform.position=new Vector3(car.transform.position.x+3, car.transform.position.y+2, car.transform.position.z);
        }
    }
}
