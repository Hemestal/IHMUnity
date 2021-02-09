using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    [SerializeField] private string selectableTag = "Selectable";
    private Transform _selection;
    public GameObject tomato,cabage;
    // Start is called before the first frame update
    void Start()
    {
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
            if(selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
                    if (Input.GetKeyDown("e"))
                    {
                        Destroy(selectionRenderer.gameObject);
                        if (selectionRenderer.gameObject.transform.parent.gameObject.name == "TomatoPlant")
                        {
                            GameObject fruit1 = Instantiate(tomato) as GameObject;
                            fruit1.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(0f, 1f, 0f);
                            GameObject fruit2 = Instantiate(tomato) as GameObject;
                            fruit2.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(0f, 1.5f, 0f);
                            GameObject fruit3 = Instantiate(tomato) as GameObject;
                            fruit3.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(0f, 2f, 0f);
                        }
                        else if (selectionRenderer.gameObject.transform.parent.gameObject.name == "Cabbages")
                        {
                            GameObject fruit1 = Instantiate(cabage) as GameObject;
                            fruit1.transform.position = selectionRenderer.gameObject.transform.position + new Vector3(0.1f, 1f, 0f);
                        }
                    }
                }
                _selection = selection;
            }
        }
    }
}
