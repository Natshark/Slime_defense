using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Slime : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;

    public GameManager GameManager;
    public GameObject Sword;
    public Slider healthBar;
    public List<Transform> goals;
    public int counter = 0;
    bool hasDestination = false;

    public float hp;
    public float physicResistance;
    public float magicResistance;

    public float timeToDeath = 1.5f;
    public int slimePrice;

    public bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Sword = GameObject.FindGameObjectWithTag("Sword");
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if (CompareTag("RedSlime"))
        {
            hp = 20;
            physicResistance = 0;
            magicResistance = 0;
            slimePrice = 10;
        }
        else
        {
            hp = 40;
            physicResistance = 0.75f;
            magicResistance = -0.25f;
            slimePrice = 20;
        }
    }

    void Update()
    {
        healthBar.value = hp;

        if (!isDead)
        {
            if (hp <= 0)
            {
                GetComponent<Animator>().speed = 0;
                GetComponent<Animator>().Play("Die");
                GetComponent<Animator>().speed = 1;
                

                isDead = true;
                healthBar.gameObject.SetActive(false);

                GameManager.PlayerMoney += slimePrice;

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
                GameManager.homeHp -= 10;
            }
        }
    }

    public void GetDamage(float damage, string typeOfDamage)
    {
        if (typeOfDamage == "physic")
        {
            hp -= damage * (1 - physicResistance);
        }
        else
        {
            hp -= damage * (1 - magicResistance);
        }

        animator.speed = 0;
        animator.Play("GetHit");
        animator.speed = 1;
    }
}
