using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public GameObject start;
    public GameObject target;
    public GameObject lightningStart;
    public GameObject lightningEnd;
    public GameObject parent;
    public GameObject createdLightning;

    public float num;
    public float level;
    public float damage;
    public string typeOfDamage = "magic";

    public float minDistance;
    public float maxDistance = 10;
    public float curDistance;

    public GameObject[] slimes;
    public List<GameObject> shockedSlimes = new List<GameObject> { };

    void Start()
    {
        if (parent.CompareTag("TeslaTower"))
        {
            start = parent.GetComponent<TowerController>().placeForMissile.gameObject;
            level = parent.GetComponent<TowerController>().level;
            num = level;
        }
        else
        {
            target = null;
            start = parent.GetComponent<Lightning>().lightningEnd.gameObject;
            num = parent.GetComponent<Lightning>().num;

            minDistance = Mathf.Infinity;
            slimes = GameObject.FindGameObjectsWithTag("RedSlime");
            findClosestSlime();

            slimes = GameObject.FindGameObjectsWithTag("BlueSlime");
            findClosestSlime();

            slimes = GameObject.FindGameObjectsWithTag("GreenSlime");
            findClosestSlime();

            slimes = GameObject.FindGameObjectsWithTag("RedSlimeBoss");
            findClosestSlime();

            slimes = GameObject.FindGameObjectsWithTag("BlueSlimeBoss");
            findClosestSlime();

            slimes = GameObject.FindGameObjectsWithTag("GreenSlimeBoss");
            findClosestSlime();
        }

        if (start && target)
        {
            damage = level * 3 * ((num + 1) / (level + 1));
            target.GetComponent<Slime>().GetDamage(damage, typeOfDamage);
            target.GetComponent<Slime>().isStunned = true;
            Invoke("stunOut", 0.5f);

            shockedSlimes.Add(target);

            if (num > 0)
            {
                Invoke("continueLightning", 0.25f);
                num -= 1;
            }
        }

        Invoke("Destroy", 0.5f);
    }

    void Update()
    {
        if (start && target)
        {
            lightningStart.transform.position = start.transform.position;
            lightningEnd.transform.position = target.transform.position;
        }
        else
        {
            stunOut();
            Destroy(gameObject);
        }
    }

    void continueLightning()
    {
        createdLightning = Instantiate(gameObject, transform.position, Quaternion.identity);
        createdLightning.GetComponent<Lightning>().parent = gameObject;
        createdLightning.GetComponent<Lightning>().shockedSlimes = shockedSlimes;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    void stunOut()
    {
        if (target)
        {
            target.GetComponent<Slime>().isStunned = false;
        }
    }

    void findClosestSlime()
    {
        foreach (GameObject slime in slimes)
        {
            curDistance = Vector3.Distance(start.transform.position, slime.transform.position);
            if (curDistance < minDistance && curDistance < maxDistance && !shockedSlimes.Contains(slime))
            {
                target = slime;
                minDistance = curDistance;
            }
        }
    }
}
