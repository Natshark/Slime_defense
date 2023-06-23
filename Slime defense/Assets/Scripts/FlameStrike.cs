using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrike : MonoBehaviour
{
    public float damage;
    public string typeOfDamage = "magic";
    public GameObject Fire;
    public GameObject createdFire;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && other.gameObject.GetComponent<Slime>().hp > 0)
        {
            other.gameObject.GetComponent<Slime>().GetDamage(damage, typeOfDamage);
            other.GetComponent<Slime>().isBurning = true;
            other.GetComponent<Slime>().burningDamage = damage / 4;
            createdFire = Instantiate(Fire, other.transform.position, Quaternion.identity);
            createdFire.GetComponent<Fire>().slime = other.gameObject;
            other.GetComponent<Slime>().Fire = createdFire;
        }
    }
}
