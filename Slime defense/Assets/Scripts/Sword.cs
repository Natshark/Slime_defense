using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject Player;
    public GameObject LastHittedSlime;

    public int damage = 5;
    public string typeOfDamage = "physic";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && other.gameObject.GetComponent<Slime>().hp > 0 && Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() == "1011701323")
        {
            if (LastHittedSlime != other.gameObject)
            {
                other.gameObject.GetComponent<Slime>().GetDamage(damage, typeOfDamage);
                
                LastHittedSlime = other.gameObject;
            }
        }
    }
}