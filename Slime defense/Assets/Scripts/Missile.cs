using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target = null;
    public GameObject hittedSlime;
    public GameObject parent;
    public Vector3 targetPosition;

    public float damage;
    public float speed;
    public string typeOfDamage;
    public int level;
    public int numOfWave;
    public float burningChance;
    public float machineGunChance;
    public GameObject Fire;
    public GameObject createdFire;

    float rand;
    void Start()
    {
        numOfWave = GameObject.FindGameObjectWithTag("Portal").GetComponent<Spawner>().numOfWave;
        level = parent.GetComponent<TowerController>().level;
        if (CompareTag("Bullet"))
        {
            damage = 2.5f + 2.5f * level;
            speed = 0.25f;
            typeOfDamage = "physic";
        }
        else
        {
            damage = 6 + 3 * level;
            speed = 0.5f;
            typeOfDamage = "magic";
            if (level < 3)
            {
                burningChance = 0.15f * (level - 1);
            }
            else
            {
                burningChance = 0.15f * 2;
            }
        }
    }

    void FixedUpdate()
    {
        if (target)
        {
            targetPosition = target.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            hittedSlime = other.gameObject;
            hittedSlime.GetComponent<Slime>().GetDamage(damage, typeOfDamage);

            if (CompareTag("Meteor") && !hittedSlime.GetComponent<Slime>().isBurning)
            {
                rand = Random.Range(0f, 1f);

                if (rand < burningChance)
                {
                    hittedSlime.GetComponent<Slime>().isBurning = true;
                    hittedSlime.GetComponent<Slime>().burningDamage = damage / 4;
                    createdFire = Instantiate(Fire, hittedSlime.transform.position, Quaternion.identity);
                    createdFire.GetComponent<Fire>().slime = hittedSlime;
                    hittedSlime.GetComponent<Slime>().Fire = createdFire;
                }
            }
            Destroy(gameObject);
        }
    }
}
