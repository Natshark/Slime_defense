using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject slime;
    public GameObject createdSlime;

    public List<Transform> globalGoals = new List<Transform> { };
    public List<Transform> localGoals;

    float timer = 0.5f;
    int counter = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            if (counter > 0)
            {
                localGoals.Clear();
                for (int i = 0; i < 6; i++)
                {
                    localGoals.Add(globalGoals[i * 3 + Random.Range(0, 3)]);
                }

                createdSlime = Instantiate(slime, transform.position, Quaternion.Euler(0, -90, 0));
                createdSlime.GetComponent<Slime>().goals = localGoals;

                counter--;
            }

            timer = 0.5f;
        }
        timer -= Time.deltaTime;
    }
}
