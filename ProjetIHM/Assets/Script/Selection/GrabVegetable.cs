using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabVegetable : MonoBehaviour
{
    private Transform _selection;
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material.shader = Shader.Find("Diffuse");
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Tomato") || selection.CompareTag("Cabbage"))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
                    if (Input.GetKeyDown("e"))
                    {
                        selection.gameObject.transform.position = new Vector3(-10.0f, 1f, 1.0f);
                    }
                }
                _selection = selection;
            }
                
        }
    }
}