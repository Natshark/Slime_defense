using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    public float rotationX = 0.0f;
    public float rotationY = 0.0f;
    

    public float sensetivityMouse = 200f;
    public Vector3 angles;
    public Transform Player;
    public Transform Head;

    private void Start()
    {
        angles = transform.eulerAngles;
        rotationX = angles.x;
        rotationY = angles.y;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * sensetivityMouse * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensetivityMouse * Time.deltaTime;

        rotationX -= mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, -30.0f, 30.0f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

        float playerRotationY = rotationY;
        Player.rotation = Quaternion.Euler(0, playerRotationY, 0);
    }
}