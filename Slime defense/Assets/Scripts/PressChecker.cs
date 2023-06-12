using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressChecker : MonoBehaviour
{

    RaycastHit hittedObject;
    Ray ray;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && transform.parent == null)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hittedObject, Mathf.Infinity, 1 << 3))
            {
                if (hittedObject.collider.gameObject.tag == "Platform")
                {
                    hittedObject.collider.gameObject.GetComponent<PlatformController>().ParticleContoller();
                }
            }
        }
        
    }


}
