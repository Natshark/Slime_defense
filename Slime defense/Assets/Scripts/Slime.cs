
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

    public int hp = 5;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Sword = GameObject.FindGameObjectWithTag("Sword");
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (hasDestination == false && counter != 6)
        {
            navMeshAgent.destination = goals[counter].position;
            hasDestination = true;
        }

        if (counter != 6 && Mathf.Abs(transform.position.x - goals[counter].position.x) < 2.5f && Mathf.Abs(transform.position.z - goals[counter].position.z) < 2.5f && hasDestination == true)
        {
            hasDestination = false;
            counter++;
        }

        if (counter != 6)
        {
            animator.Play("WalkFWD");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
