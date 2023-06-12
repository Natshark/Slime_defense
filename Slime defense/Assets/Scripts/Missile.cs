using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target = null;
    public Vector3 targetPosition;

    public int damage = 5;
    public float speed = 0.5f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (target)
        {
            targetPosition = target.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Slime")
        {
            other.gameObject.GetComponent<Slime>().hp -= damage;
            other.gameObject.GetComponent<Animator>().speed = 0;
            other.gameObject.GetComponent<Animator>().Play("GetHit");
            other.gameObject.GetComponent<Animator>().speed = 1;
            Destroy(gameObject);
        }
    }
}
