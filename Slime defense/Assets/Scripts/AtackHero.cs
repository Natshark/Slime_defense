using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class AtackHero : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Sword;

    public float CoolDownHit = 0f;

    void Update()
    {
        if (CoolDownHit <= 0 && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash.ToString() != "-86853306")
        {
            Attack();
        }

        CoolDownHit -= Time.deltaTime;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.transform.parent != null)
            {
                GetComponent<Animator>().Play("Attack01_SwordAndShiled");
                CoolDownHit = 0f;
                Sword.GetComponent<Sword>().LastHittedSlime = null;
            }
        } 
    }
}