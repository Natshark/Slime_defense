
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Slime : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;

    public GameObject Sword;
    public List<Transform> goals;
    public int counter = 0;
    bool hasDestination = false;

    public int hp = 20;
    public float timeToDeath = 1.5f;

    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Sword = GameObject.FindGameObjectWithTag("Sword");
        
    }

    void Update()
    {
        if (!isDead)
        {
            if (hp <= 0)
            {
                GetComponent<Animator>().speed = 0;
                GetComponent<Animator>().Play("Die");
                GetComponent<Animator>().speed = 1;

                isDead = true;
                timeToDeath -= Time.deltaTime;
                navMeshAgent.destination = transform.position;
                navMeshAgent.speed = 0;
            }
        }
        else
        {
            timeToDeath -= Time.deltaTime;
        }

        if (timeToDeath <= 0)
        {
            Destroy(gameObject);
        }

        if (hasDestination == false && counter != 7)
        {
            navMeshAgent.destination = goals[counter].position;
            hasDestination = true;
        }

        if (counter != 7 && Mathf.Abs(transform.position.x - navMeshAgent.destination.x) < 2.5f && Mathf.Abs(transform.position.z - navMeshAgent.destination.z) < 2.5f && hasDestination == true)
        {
            hasDestination = false;
            counter++;
        }

        if (hp > 0)
        {
            if (counter != 7)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "-1996668047")
                {

                    animator.Play("WalkFWD");
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }
}
