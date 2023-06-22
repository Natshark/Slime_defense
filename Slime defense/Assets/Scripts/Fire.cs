using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject slime;

    void Start()
    {
        if (slime.CompareTag("BlueSlime"))
        {
            transform.localScale *= 2;
        }
        else if (slime.CompareTag("RedSlimeBoss"))
        {
            transform.localScale *= 10f;
        }
        else if (slime.CompareTag("BlueSlimeBoss"))
        {
            transform.localScale *= 12.5f;
        }
        else if (slime.CompareTag("GreenSlimeBoss"))
        {
            transform.localScale *= 12.5f;
        }
    }

    void Update()
    {
        if (slime)
        {
            transform.position = slime.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
