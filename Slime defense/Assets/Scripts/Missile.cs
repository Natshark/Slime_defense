using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target = null;
    public GameObject parent;
    public Vector3 targetPosition;

    public float damage;
    public float speed;
    public string typeOfDamage;
    public int level;
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
        if (other.gameObject.layer == 11 && other.gameObject.GetComponent<Slime>().hp > 0)
        {
            other.gameObject.GetComponent<Slime>().GetDamage(damage, typeOfDamage);
            Destroy(gameObject);
        }
    }
}
