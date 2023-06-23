using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject Player;
    public int numOfWave;
    public GameObject LastHittedSlime;

    public float damage;
    public string typeOfDamage = "physic";

    private void Start()
    {
        damage = 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11 && Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() == "1011701323")
        {
            if (LastHittedSlime != other.gameObject)
            {
                other.gameObject.GetComponent<Slime>().GetDamage(damage, typeOfDamage);
                
                LastHittedSlime = other.gameObject;
            }
        }
    }
}