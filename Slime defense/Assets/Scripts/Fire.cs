using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject slime;

    void Start()
    {
        
    }

    void Update()
    {
        if (slime)
        {
            transform.position = slime.transform.position;
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}
