using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerController : MonoBehaviour
{
    public GameObject missile;
    public GameObject target;
    public GameObject rotatableObject;
    public GameObject createdMissile;
    public Transform placeForMissile;

    Vector3 direction;

    public bool hasTarget = false;

    public float attackCoolDown;
    public float timer = 0;
    public int level = 1;

    public float minDistance;
    public float curDistance;

    public List<GameObject> targets = new List<GameObject> { };
    public List<GameObject> realTargets = new List<GameObject> { };

    private void Start()
    {
        if (CompareTag("CannonTower"))
        {
            attackCoolDown = 0.75f;
        }
        else if (CompareTag("MagicTower"))
        {
            attackCoolDown = 1;
        }
        else
        {
            attackCoolDown = 1.5f;
        }
    }

    void Update()
    {
        if (target)
        {
            if (target.GetComponent<Slime>().hp <= 0)
            {
                if (targets.Contains(target))
                {
                    targets.Remove(target);
                    target = null;
                }
            }
            else
            {
                direction = target.transform.position - transform.position;
                direction.y = 0;
                rotatableObject.transform.rotation = Quaternion.Slerp(rotatableObject.transform.rotation, Quaternion.LookRotation(direction), 5 * Time.deltaTime);

                if (timer <= 0)
                {
                    timer = attackCoolDown;
                    createdMissile = Instantiate(missile, placeForMissile.position, Quaternion.identity);
                    if (!createdMissile.CompareTag("Lightning"))
                    {
                        createdMissile.GetComponent<Missile>().target = target;
                    }
                    else
                    {
                        createdMissile.GetComponent<Lightning>().target = target;
                        createdMissile.GetComponent<Lightning>().parent = gameObject;
                    }
                }
            }
        }

        target = null;
        realTargets.Clear();
        foreach (GameObject slime in targets)
        {
            if (slime != null)
            {
                realTargets.Add(slime);
            }
        }

        minDistance = float.PositiveInfinity;
        foreach (GameObject slime in realTargets)
        {
            curDistance = Vector3.Distance(transform.position, slime.transform.position);
            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                target = slime;
            }
        }

        timer -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && !targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11 && targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }

}
