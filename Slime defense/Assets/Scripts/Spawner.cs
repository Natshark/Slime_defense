using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject redSlime;
    public GameObject blueSlime;
    public GameObject greenSlime;
    public GameObject redSlimeBoss;
    public GameObject blueSlimeBoss;
    public GameObject greenSlimeBoss;
    public GameObject chosenSlime;
    public GameObject createdSlime;


    List<float> slimeChanses = new List<float> { };
    List<float> slimeBossChanses = new List<float> { };
    List<float> waveEndChanses = new List<float> { };

    public List<Transform> globalGoalsNorth = new List<Transform> { };
    public List<Transform> globalGoalsSouth = new List<Transform> { };
    public List<Transform> localGoals;

    float slimeSpawnCoolDown;
    float slimeBossSpawnCoolDown;
    float timer;
    int slimeCounter;
    int slimeBossCounter;
    public int numOfWave;

    float rand;
    void Start()
    {
        slimeSpawnCoolDown = 3;
        slimeBossSpawnCoolDown = 10;
        timer = slimeSpawnCoolDown;
        slimeCounter = 20;
        numOfWave = 1;

        setSlimeChanses();
        setWaveEndChanses();
    }

    void Update()
    {
        if (timer <= 0)
        {
            localGoals.Clear();
            for (int i = 0; i < 7; i++)
            {
                localGoals.Add(globalGoalsNorth[i * 3 + Random.Range(0, 3)]);
            }

            if (slimeCounter > 0)
            {
                choiceOfSlime();

                createdSlime = Instantiate(chosenSlime, transform.position, Quaternion.Euler(0, -90, 0));
                createdSlime.GetComponent<Slime>().goals = localGoals;

                slimeCounter--;
                timer = slimeSpawnCoolDown; 
            }
            else if (slimeBossCounter > 0)
            {
                choiceOfWaveEnd();

                createdSlime = Instantiate(chosenSlime, transform.position, Quaternion.Euler(0, -90, 0));
                createdSlime.GetComponent<Slime>().goals = localGoals;

                slimeBossCounter--;
                timer = slimeBossSpawnCoolDown;
            }
            else
            {
                timer = 30;
                slimeCounter = 20;
                numOfWave++;
                setSlimeChanses();
                setWaveEndChanses();
            } 
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void setSlimeChanses()
    {
        slimeChanses.Clear();
        slimeBossChanses.Clear();

        if (numOfWave == 1)
        {
            slimeChanses.Add(0.95f);
            slimeChanses.Add(1f);
            slimeChanses.Add(0f);

            slimeBossChanses.Add(0f);
            slimeBossChanses.Add(0f);
            slimeBossChanses.Add(0f);
        }
        else if (numOfWave == 2)
        {
            slimeChanses.Add(0.75f);
            slimeChanses.Add(1f);
            slimeChanses.Add(0f);

            slimeBossChanses.Add(0f);
            slimeBossChanses.Add(0f);
            slimeBossChanses.Add(0f);
        }
        else if (numOfWave == 3)
        {
            slimeChanses.Add(0.5f);
            slimeChanses.Add(0.85f);
            slimeChanses.Add(1f);

            slimeBossChanses.Add(0f);
            slimeBossChanses.Add(0f);
            slimeBossChanses.Add(0f);
        }
        else if (numOfWave == 4)
        {
            slimeChanses.Add(0.35f);
            slimeChanses.Add(0.7f);
            slimeChanses.Add(0.95f);

            slimeBossChanses.Add(1f);
            slimeBossChanses.Add(0f);
            slimeBossChanses.Add(0f);
        }
        else if (numOfWave == 5)
        {
            slimeChanses.Add(0.25f);
            slimeChanses.Add(0.55f);
            slimeChanses.Add(0.85f);

            slimeBossChanses.Add(0.9f);
            slimeBossChanses.Add(0.95f);
            slimeBossChanses.Add(1f);
        }
        else
        {
            slimeChanses.Add(0.05f);
            slimeChanses.Add(0.45f);
            slimeChanses.Add(0.85f);

            slimeBossChanses.Add(0.9f);
            slimeBossChanses.Add(0.95f);
            slimeBossChanses.Add(1f);
        }
    }
    void choiceOfSlime()
    {
        rand = Random.Range(0f, 1f);

        if (rand < slimeChanses[0])
        {
            chosenSlime = redSlime;
        }
        else if (rand < slimeChanses[1])
        {
            chosenSlime = blueSlime;
        }
        else if (rand < slimeChanses[2])
        {
            chosenSlime = greenSlime;
        }
        else if (rand < slimeBossChanses[0])
        {
            chosenSlime = redSlimeBoss;
        }
        else if (rand < slimeBossChanses[1])
        {
            chosenSlime = blueSlimeBoss;
        }
        else
        {
            chosenSlime = greenSlimeBoss;
        }
    }

    void setWaveEndChanses()
    {
        waveEndChanses.Clear();
        if (numOfWave == 1)
        {
            slimeBossCounter = 1;
            waveEndChanses.Add(1);
            waveEndChanses.Add(0);
            waveEndChanses.Add(0);
        }
        else if (numOfWave == 2)
        {
            slimeBossCounter = 3;
            waveEndChanses.Add(1);
            waveEndChanses.Add(0);
            waveEndChanses.Add(0);
        }
        else if (numOfWave == 3)
        {
            slimeBossCounter = 5;
            waveEndChanses.Add(0.75f);
            waveEndChanses.Add(1);
            waveEndChanses.Add(0);
        }
        else if (numOfWave == 4)
        {
            slimeBossCounter = 10;
            waveEndChanses.Add(0.5f);
            waveEndChanses.Add(0.75f);
            waveEndChanses.Add(1);
        }
        else if (numOfWave == 5)
        {
            slimeBossCounter = 10;
            waveEndChanses.Add(0.25f);
            waveEndChanses.Add(0.5f);
            waveEndChanses.Add(1);
        }
        else
        {
            slimeBossCounter = 10 + numOfWave;
            waveEndChanses.Add(0.2f);
            waveEndChanses.Add(0.6f);
            waveEndChanses.Add(1);
        }
    }

    void choiceOfWaveEnd()
    {
        rand = Random.Range(0f, 1f);

        if (rand < waveEndChanses[0])
        {
            chosenSlime = redSlimeBoss;
        }
        else if (rand < waveEndChanses[1])
        {
            chosenSlime = blueSlimeBoss;
        }
        else
        {
            chosenSlime = greenSlimeBoss;
        }
    }
}
