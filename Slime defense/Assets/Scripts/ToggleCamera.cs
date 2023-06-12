using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject TowerUI;
    public Transform PlaceForCamera;
    Vector3 startPosition = new Vector3 (30.18f, 28.5f, 11.2f);
    Quaternion startRotation;
    bool canvasActivity;
    void Start()
    {
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (transform.parent == null)
            {
                canvasActivity = TowerUI.GetComponent<Canvas>().enabled;
                
                transform.parent = player.transform;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                GetComponent<CameraRotation>().rotationX = 0;
                GetComponent<CameraRotation>().rotationY = 0;
                transform.position = PlaceForCamera.position;

                GetComponent<CameraRotation>().enabled = true;
                player.GetComponent<PlayerMovement>().enabled = true;
                TowerUI.GetComponent<Canvas>().enabled = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                transform.parent = null;
                transform.position = startPosition;
                transform.rotation = startRotation;

                GetComponent<CameraRotation>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
                TowerUI.GetComponent<Canvas>().enabled = canvasActivity;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
