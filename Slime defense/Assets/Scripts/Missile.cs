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
    public float burningChance;
    public GameObject Fire;
    public GameObject createdFire;

    float rand;
    void Start()
    {
        level = parent.GetComponent<TowerController>().level;
        if (CompareTag("Bullet"))
        {
            damage = 2.5f + 2.5f * level;
            speed = 0.25f;
            typeOfDamage = "physic";
        }
        else
        {
            damage = 5 + 5 * level;
            speed = 0.5f;
            typeOfDamage = "magic";
            burningChance = 0.05f + 0.05f * level;
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
            if (CompareTag("Meteor"))
            {
                rand = Random.Range(0, 1);

                if (rand < burningChance)
                {
                    hittedSlime.GetComponent<Slime>().isBurning = true;
                    hittedSlime.GetComponent<Slime>().burningDamage = level;
                    createdFire = Instantiate(Fire, hittedSlime.transform.position, Quaternion.identity);
                    createdFire.GetComponent<Fire>().slime = hittedSlime;
                    hittedSlime.GetComponent<Slime>().Fire = createdFire;
                }
            }
            Destroy(gameObject);
        }
    }
}
