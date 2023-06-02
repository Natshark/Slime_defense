using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpPower = 200f;

    public bool isGround;

    public Rigidbody rb;

    private void Update()
    {
        GetInput();
    }

    private void GetInput() 
    {
        // Проверка на зажатие
        if (Input.GetKey(KeyCode.W)) 
        {
            // Текущая позиция объекта скрипта += вектор вперед * скорость * deltaTime
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += -transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            rb.AddForce(transform.up * jumpPower);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Ground") 
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
}
