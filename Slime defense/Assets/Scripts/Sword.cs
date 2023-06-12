using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject Player;
    public GameObject LastHittedSlime;

    public int damage = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slime" && other.gameObject.GetComponent<Slime>().hp > 0 && Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() == "1011701323")
        {
            if (LastHittedSlime != other.gameObject)
            {
                other.gameObject.GetComponent<Slime>().hp -= damage;
                other.gameObject.GetComponent<Animator>().speed = 0;
                other.gameObject.GetComponent<Animator>().Play("GetHit");
                other.gameObject.GetComponent<Animator>().speed = 1;
                LastHittedSlime = other.gameObject;
            }
        }
    }
}