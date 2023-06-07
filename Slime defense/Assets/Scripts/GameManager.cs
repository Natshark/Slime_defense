using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject lastPressedPlatform;
    public GameObject magicTower;
    public GameObject cannonTower;

    public void createMagicTower()
    {
        Instantiate(magicTower, lastPressedPlatform.transform.position, Quaternion.identity);
    }

    public void createCannonTower()
    {
        Instantiate(cannonTower, lastPressedPlatform.transform.position, Quaternion.identity);
    }
}
