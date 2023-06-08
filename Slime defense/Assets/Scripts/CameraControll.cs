using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;


    public float sensetivityMouse = 200f;

    public Transform Player;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.x;
        rotationY = angles.y;
        
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensetivityMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensetivityMouse * Time.deltaTime;

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

        float playerRotationY = rotationY - 180.0f;
        Player.transform.rotation = Quaternion.Euler(0, playerRotationY, 0);
    }
}