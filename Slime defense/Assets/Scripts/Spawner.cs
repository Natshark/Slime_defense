using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject redSlime;
    public GameObject blueSlime;
    public GameObject greenSlime;
    public GameObject chosenSlime;
    public GameObject createdSlime;

    public List<Transform> globalGoalsNorth = new List<Transform> { };
    public List<Transform> globalGoalsSouth = new List<Transform> { };
    public List<Transform> localGoals;

    float spawnCoolDown = 2f;
    float timer;
    int counter = 100;

    float rand;
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

                rand = Random.Range(0f, 1f);
                if (rand < 0.5f)
                {
                    chosenSlime = redSlime;
                }
                else if (rand < 0.75f)
                {
                    chosenSlime = blueSlime;
                }
                else
                {
                    chosenSlime = greenSlime;
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
