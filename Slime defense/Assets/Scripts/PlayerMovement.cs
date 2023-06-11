using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody rb;
    public Animator animator;

    public bool isMoving = false;

    private void Update()
    {
        GetInput();

        if (isMoving)
        {
            animator.Play("MoveFWD_Battle_InPlace_SwordAndShield");
        }
        else if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "1011701323")
        {
            GetComponent<Animator>().speed = 0;
            animator.Play("Idle_Battle_SwordAndShield");
            GetComponent<Animator>().speed = 1;
        }
    }

    private void GetInput() 
    {
        isMoving = false;
        // Проверка на зажатие
        if (Input.GetKey(KeyCode.W)) 
        {
            // Текущая позиция объекта скрипта += вектор вперед * скорость * deltaTime
            transform.localPosition += transform.forward * speed * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += -transform.right * speed * Time.deltaTime;
            isMoving = true;
        }
    }
}
