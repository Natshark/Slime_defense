using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    Animator animator;

    public float hp = 100;
    public bool isDead = false;

    public GameObject triggerredSlime = null;
    GameObject closestSlime = null;
    public GameObject[] slimes;
    float mindist;
    float curdist;

    float deathCoolDown = 10f;
    float timer;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            if (hp <= 0)
            {
                Camera.main.GetComponent<ToggleCamera>().thirdFaceView();
                triggerredSlime = null;

                animator.speed = 0;
                animator.Play("Die01_SwordAndShield");
                animator.speed = 1;

                isDead = true;
                timer = deathCoolDown;
            }
            else
            {
                slimes = GameObject.FindGameObjectsWithTag("RedSlime");
                mindist = Mathf.Infinity;
                foreach (GameObject slime in slimes)
                {
                    curdist = Vector3.Distance(transform.position, slime.transform.position);
                    if (curdist < mindist)
                    {
                        mindist = curdist;
                        closestSlime = slime;
                    }
                }
                slimes = GameObject.FindGameObjectsWithTag("BlueSlime");
                foreach (GameObject slime in slimes)
                {
                    curdist = Vector3.Distance(transform.position, slime.transform.position);
                    if (curdist < mindist)
                    {
                        mindist = curdist;
                        closestSlime = slime;
                    }
                }

                if (closestSlime && Vector3.Distance(transform.position, closestSlime.transform.position) <= 7.5f)
                {
                    closestSlime.GetComponent<Slime>().target = gameObject;
                    triggerredSlime = closestSlime;
                }
                else
                {
                    triggerredSlime = null;
                }
            }
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            hp = 100;
            isDead = false;

            animator.speed = 0;
            animator.Play("GetUp_SwordAndShield");
            animator.speed = 1;
        }
    }
}
