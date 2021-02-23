using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public GameObject player;
    public GameObject Cam;
    private bool pushT = false, pushF = true;

    public void PushCube(GameObject Box)
    {
        Rigidbody RigidBox = Box.GetComponent<Rigidbody>();
        if (pushF)
        {
            RigidBox.isKinematic = true;
            transform.SetParent(Cam.transform);
            transform.localPosition = new Vector3(0, 0, 2);
            pushF = false;
            pushT = true;
        }

        else if (pushT)
        {
            transform.parent = null;
            RigidBox.isKinematic = false;
            pushF = true;
            pushT = false;
        }
    }
}
