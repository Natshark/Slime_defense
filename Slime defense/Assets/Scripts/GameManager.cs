using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject lastPressedPlatform;
    public Canvas CanvasOfTowerUI;
    public GameObject magicTower;
    public GameObject cannonTower;
    public bool hasTower;

    public int PlayerMoney;
    public Text moneyText;

    public int magicTowerPrice = 50;
    public int cannonTowerPrice = 30;

    void Start()
    {
        PlayerMoney = 0;
    }

    void Update()
    {
        moneyText.text = PlayerMoney.ToString();
    }

    public void createMagicTower()
    {
        if (!hasTower && PlayerMoney >= magicTowerPrice) 
        {
            PlayerMoney -= magicTowerPrice;

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
        if (!hasTower && PlayerMoney >= cannonTowerPrice)
        {
            PlayerMoney -= cannonTowerPrice;

            Instantiate(cannonTower, lastPressedPlatform.transform.position, Quaternion.identity);
            lastPressedPlatform.GetComponent<PlatformController>().hasTower = true;
            CanvasOfTowerUI.enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
            lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }
}
