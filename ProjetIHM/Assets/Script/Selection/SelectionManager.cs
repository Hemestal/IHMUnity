using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    private Transform _selection;
    public GameObject tomato,cabage;
    public TextMeshPro TomatoText, CabbageText;
    public int TomatoCounter, CabbageCounter;
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
            if (selection.CompareTag("Selectable") || selection.CompareTag("Tomato") || selection.CompareTag("Cabbage"))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
                    if (Input.GetKeyDown("e"))
                    {
                        if (selection.CompareTag("Tomato"))
                        {
                            if (Input.GetKeyDown("e"))
                            {
                                TomatoCounter++;
                                selection.gameObject.transform.position = new Vector3(-10.0f, 1f, 1.0f);
                                TomatoText.transform.GetComponent<TextMeshPro>().text =  TomatoCounter + "\nTomatoes";
                            }
                        }
                        else if(selection.CompareTag("Cabbage"))
                        {
                            if (Input.GetKeyDown("e"))
                            {
                                CabbageCounter++;
                                selection.gameObject.transform.position = new Vector3(10.0f, 1f, 1.0f);
                                CabbageText.transform.GetComponent<TextMeshPro>().text = CabbageCounter + "\nCabbages";
                            }
                        }
                        else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "TomatoPlant")
                        {
                            Destroy(selectionRenderer.gameObject);
                            GameObject fruit1 = Instantiate(tomato) as GameObject;
                            fruit1.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 1f, 0f);
                            GameObject fruit2 = Instantiate(tomato) as GameObject;
                            fruit2.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 1.5f, 0f);
                            GameObject fruit3 = Instantiate(tomato) as GameObject;
                            fruit3.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f, 0.4f), 2f, 0f);
                        }
                        else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "Cabbages")
                        {
                            Destroy(selectionRenderer.gameObject);
                            GameObject fruit1 = Instantiate(cabage) as GameObject;
                            fruit1.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(Random.Range(-0.4f,0.4f), 1f, 0f);
                        }
                    }
                }
                _selection = selection;
            }
        }
    }
}
