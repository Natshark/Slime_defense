using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{
    public GameManager GameManager;
    public GameObject TowerUI;
    public GameObject pressedObject;
    RaycastHit hit;
    Ray ray;
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                pressedObject = hit.collider.gameObject;

                if (pressedObject.tag != "Platform" && pressedObject.tag != "Button")
                {
                    TowerUI.GetComponent<Canvas>().enabled = false;
                    if (GameManager.lastPressedPlatform != null)
                    {
                        GameManager.lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                    }
                }
            }
        }
    }
}
