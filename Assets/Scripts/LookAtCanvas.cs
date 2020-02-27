using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCanvas : MonoBehaviour
{

	void Update ()
    {
        //transform.LookAt(Camera.main.transform);// = Quaternion.Euler(Vector3.zero);
        //transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.LookAt(Camera.main.transform.eulerAngles + transform.position + Vector3.back);
        //transform.right = -transform.parent.right;
        //transform.position = 
    }
}
