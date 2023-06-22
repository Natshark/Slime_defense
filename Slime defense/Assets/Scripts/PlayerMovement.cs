using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 35;
    public Rigidbody rb;
    public Animator animator;
    public string currentAnimatorState;

    public bool isMoving = false;
    void Start()
    {
        speed *= 1000;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GetComponent<Player>().isDead)
        {
            currentAnimatorState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString();
            GetInput();

            if (isMoving)
            {
                animator.Play("MoveFWD_Battle_InPlace_SwordAndShield");
            }
            else if (currentAnimatorState != "1011701323" && currentAnimatorState != "-1550725538") // currentAnimatorState != "-86853306"
            {
                animator.speed = 0;
                animator.Play("Idle_Battle_SwordAndShield");
                animator.speed = 1;
            }
        }
    }

    void GetInput() 
    {
        // Проверка на паузе ли игра
        if (Time.timeScale == 0f) { return; }

        isMoving = false;
        // Проверка на зажатие
        if (Input.GetKey(KeyCode.W)) 
        {
            // Текущая позиция объекта скрипта += вектор вперед * скорость * deltaTime
            rb.AddForce(transform.forward * speed * Time.deltaTime);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed * Time.deltaTime);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * speed * Time.deltaTime);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * speed * Time.deltaTime);
            isMoving = true;
        }
    }
}
