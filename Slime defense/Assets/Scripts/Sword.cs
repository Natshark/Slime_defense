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
        if (other.gameObject.tag == "Slime" && Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() == "1011701323")
        {
            if (LastHittedSlime != other.gameObject)
            {
                other.gameObject.GetComponent<Slime>().hp -= damage;
                LastHittedSlime = other.gameObject;
            }
        }
    }
}