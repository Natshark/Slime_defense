using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerHealthBar : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
