using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject redSlime;
    public GameObject blueSlime;
    public GameObject chosenSlime;
    public GameObject createdSlime;

    public List<Transform> globalGoalsNorth = new List<Transform> { };
    public List<Transform> globalGoalsSouth = new List<Transform> { };
    public List<Transform> localGoals;

    float spawnCoolDown = 1.5f;
    float timer;
    int counter = 100;

    int rand;
    void Start()
    {

    }

    void Update()
    {
        if (timer <= 0)
        {
            if (counter > 0)
            {
                localGoals.Clear();
                for (int i = 0; i < 7; i++)
                {
                    localGoals.Add(globalGoalsNorth[i * 3 + Random.Range(0, 3)]);
                }

                rand = Random.Range(1, 5);
                if (rand < 4)
                {
                    chosenSlime = redSlime;
                }
                else
                {
                    chosenSlime = blueSlime;
                }
                createdSlime = Instantiate(chosenSlime, transform.position, Quaternion.Euler(0, -90, 0));
                createdSlime.GetComponent<Slime>().goals = localGoals;

                counter--;
            }

            timer = spawnCoolDown;
        }
        timer -= Time.deltaTime;
    }
}
