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

    public void createMagicTower()
    {
        Instantiate(magicTower, lastPressedPlatform.transform.position, Quaternion.identity);
        CanvasOfTowerUI.enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
        lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
    }

    public void createCannonTower()
    {
        Instantiate(cannonTower, lastPressedPlatform.transform.position, Quaternion.identity);
        CanvasOfTowerUI.enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
        lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
    }
}
