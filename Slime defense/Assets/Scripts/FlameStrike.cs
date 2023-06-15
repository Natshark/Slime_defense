using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrike : MonoBehaviour
{
    public int damage = 20;
    public string typeOfDamage = "magic";
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && other.gameObject.GetComponent<Slime>().hp > 0)
        {
            other.gameObject.GetComponent<Slime>().GetDamage(damage, typeOfDamage);
        }
    }
}
