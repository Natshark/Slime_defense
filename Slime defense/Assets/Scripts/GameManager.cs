using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject lastPressedPlatform;
    public Canvas CanvasOfTowerUI;
    public GameObject magicTower;
    public GameObject cannonTower;
    public bool hasTower;

    public void createMagicTower()
    {
        if (!hasTower) 
        {
            Instantiate(magicTower, lastPressedPlatform.transform.position, Quaternion.identity);
            lastPressedPlatform.GetComponent<PlatformController>().hasTower = true;
            CanvasOfTowerUI.enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
            lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }

    public void createCannonTower()
    {
        if (!hasTower)
        {
            Instantiate(cannonTower, lastPressedPlatform.transform.position, Quaternion.identity);
            lastPressedPlatform.GetComponent<PlatformController>().hasTower = true;
            CanvasOfTowerUI.enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
            lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }
}
