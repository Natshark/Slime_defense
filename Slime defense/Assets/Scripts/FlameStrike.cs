using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameStrike : MonoBehaviour
{
    public int damage = 20;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slime" && other.gameObject.GetComponent<Slime>().hp > 0)
        {
            other.gameObject.GetComponent<Slime>().hp -= damage;
            other.gameObject.GetComponent<Animator>().speed = 0;
            other.gameObject.GetComponent<Animator>().Play("GetHit");
            other.gameObject.GetComponent<Animator>().speed = 1;
        }
    }
}
