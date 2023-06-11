using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody rb;
    public Animator animator;

    private void Update()
    {
        GetInput();
    }

    private void GetInput() 
    {
        // �������� �� �������
        if (Input.GetKey(KeyCode.W)) 
        {
            // ������� ������� ������� ������� += ������ ������ * �������� * deltaTime
            transform.localPosition += transform.forward * speed * Time.deltaTime;
            animator.Play("MoveFWD_Battle_InPlace_SwordAndShield");
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
            animator.Play("MoveFWD_Battle_InPlace_SwordAndShield");
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
            animator.Play("MoveFWD_Battle_InPlace_SwordAndShield");
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += -transform.right * speed * Time.deltaTime;
            animator.Play("MoveFWD_Battle_InPlace_SwordAndShield");
        }

    }
}
