using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTower : MonoBehaviour
{
    public GameObject missile;
    public GameObject target;
    public GameObject rotatableObject;
    public GameObject createdMissile;
    public Transform placeForMissile;

    Vector3 direction;

    public bool hasTarget = false;

    public float attackCoolDown = 1;
    public float timer = 0;


    void Update()
    {
        if (target)
        {
            rotatableObject.transform.LookAt(target.transform.position);

            direction = target.transform.position - transform.position;
            direction.y = 0;
            rotatableObject.transform.rotation = Quaternion.Slerp(rotatableObject.transform.rotation, Quaternion.LookRotation(direction), 5 * Time.deltaTime);

            if (timer <= 0)
            {
                timer = attackCoolDown;
                createdMissile = Instantiate(missile, placeForMissile.position, Quaternion.identity);
                createdMissile.GetComponent<Missile>().target = target;
            }
        }

        timer -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasTarget && other.gameObject.tag == "Slime")
        {
            target = other.gameObject;
            hasTarget = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!hasTarget && other.gameObject.tag == "Slime")
        {
            target = other.gameObject;
            hasTarget = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (hasTarget && other.gameObject.tag == "Slime")
        {
            target = null;
            hasTarget = false;
        }
    }

}
