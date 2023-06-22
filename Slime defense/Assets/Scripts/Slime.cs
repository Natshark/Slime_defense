using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Slime : MonoBehaviour
{
    Animator animator;
    public NavMeshAgent navMeshAgent;

    Vector3 currentRoadDestination;

    public GameManager GameManager;
    public GameObject Sword;
    public GameObject target;
    public GameObject Player;
    public Slider healthBar;
    public GameObject Fire;
    public List<Transform> goals;
    public int counter = 0;
    public int areaMask = 1 << 0;
    bool hasDestination = false;
    public bool isStunned = false;
    public bool isBurning = false;
    public int burningCounter = 5;
    public float burningDamage;
    public float burningTimer = 1;

    public float hp;
    public float physicResistance;
    public float magicResistance;
    public float damage;
    public float damageToHome;

    public float timeToDeath = 1.5f;
    public float attackCoolDown = 2f;
    public float timer = 0;
    public float curDist;
    public float minAttackDist;
    public int slimePrice;

    public bool isPlayerInAttackZone = false;
    public bool isDead = false;
 
    Ray ray;
    RaycastHit hit;
    public GameObject hittedObject;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Sword = GameObject.FindGameObjectWithTag("Sword");
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Player = GameObject.FindGameObjectWithTag("Player");

        if (CompareTag("RedSlime"))
        {
            hp = 20;
            physicResistance = 0;
            magicResistance = 0;
            damage = 20;
            damageToHome = 10;
            slimePrice = 10;
            minAttackDist = 2f;
        }
        else
        {
            hp = 40;
            physicResistance = 0.75f;
            magicResistance = -0.25f;
            damage = 50;
            damageToHome = 20;
            slimePrice = 20;
            minAttackDist = 3.5f;
        }
    }

    void Update()
    {
        healthBar.value = hp;

        if (!isDead)
        {
            if (hp <= 0)
            {
                Death();
            }
            else if (counter != 7) // если слайм не дошёл до конца пути
            {
                if (!isStunned)
                {
                    navMeshAgent.speed = 3.5f;
                }
                else
                {
                    navMeshAgent.speed = 0;
                }

                if (isBurning)
                {
                    Burning();
                }

                if (Player.GetComponent<Player>().triggerredSlime != gameObject)
                {
                    if (!hasDestination)
                    {
                        currentRoadDestination = goals[counter].position;
                        hasDestination = true;
                    }
                    else if (Mathf.Abs(transform.position.x - navMeshAgent.destination.x) < 2.5f && Mathf.Abs(transform.position.z - navMeshAgent.destination.z) < 2.5f)
                    {
                        navMeshAgent.areaMask = 1 << 0;
                        hasDestination = false;
                        counter++;
                    }

                    navMeshAgent.destination = currentRoadDestination;
                }
                else
                {
                    navMeshAgent.areaMask = 1 << 0 | 1 << 3;

                    attackZoneCheck();

                    if (!isPlayerInAttackZone)
                    {
                        navMeshAgent.destination = Player.transform.position;
                    }
                    else if (!isStunned)
                    {
                        navMeshAgent.speed = 0f;
                        transform.LookAt(target.transform);

                        if (timer <= 0)
                        {
                            ray = new Ray(transform.position + transform.up, transform.forward);
                            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6 | 1 << 12))
                            {
                                hittedObject = hit.collider.gameObject;

                                if (hittedObject.CompareTag("Player"))
                                {
                                    if (hittedObject.GetComponent<Player>().hp > 0)
                                    {
                                        hittedObject.GetComponent<Player>().hp -= damage;
                                    }
                                }
                            }

                            animator.speed = 0;
                            animator.Play("Attack01");
                            animator.speed = 1;

                            timer = attackCoolDown;
                        }
                    }
                }

                if (animator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "-1996668047" && animator.GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "-1520425993")
                {
                    animator.Play("WalkFWD");
                }
            }
            else
            {
                if (Player.GetComponent<Player>().triggerredSlime != gameObject)
                {
                    Player.GetComponent<Player>().triggerredSlime = null;
                }

                Destroy(gameObject);
                if (GameManager.homeHp > 0)
                {
                    GameManager.homeHp -= damageToHome;
                }
            }
        }
        else
        {
            timeToDeath -= Time.deltaTime;
            if (timeToDeath <= 0)
            {
                if (Player.GetComponent<Player>().triggerredSlime != gameObject)
                {
                    Player.GetComponent<Player>().triggerredSlime = null;
                }
                Destroy(gameObject);
            }
        }

        timer -= Time.deltaTime;
    }

    public void GetDamage(float damage, string typeOfDamage)
    {
        if (hp > 0)
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

    void attackZoneCheck()
    {
        curDist = Vector3.Distance(transform.position, target.transform.position);

        if (curDist > minAttackDist)
        {
            isPlayerInAttackZone = false;
        }
        else
        {
            isPlayerInAttackZone = true;
        }
    }

    public void Burning()
    {
        if (burningTimer <= 0)
        {
            if (burningCounter > 0)
            {
                GetDamage(burningDamage, "magic");
                burningCounter--;
            }
            else
            {
                isBurning = false;
                burningCounter = 5;
                if (Fire)
                {
                    Destroy(Fire);
                }
            }
            burningTimer = 1;
        }
        else
        {
            burningTimer -= Time.deltaTime;
        }
    }

    void Death()
    {
        animator.speed = 0;
        animator.Play("Die");
        animator.speed = 1;


        isDead = true;
        healthBar.gameObject.SetActive(false);

        if (target != null)
        {
            Player.GetComponent<Player>().triggerredSlime = null;
        }
        target = null;

        GameManager.PlayerMoney += slimePrice;

        navMeshAgent.destination = transform.position;
        navMeshAgent.speed = 0;
    }
}
