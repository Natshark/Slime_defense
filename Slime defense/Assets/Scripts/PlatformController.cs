using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour
{
    public GameObject Camera;
    public Canvas CanvasOfTowerUI;
    public GameManager GameManager;
    public GameObject lastPressedPlatform;
    public GameObject platformParticle;
    public GameObject CurrentPlatformParticle;

    public bool hasTower = false;

    void Start()
    {
        CanvasOfTowerUI = GameObject.FindGameObjectWithTag("TowerUI").GetComponent<Canvas>();
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        CurrentPlatformParticle = Instantiate(platformParticle, transform.position, Quaternion.identity);
        CurrentPlatformParticle.GetComponent<ParticleSystem>().Stop();
        CurrentPlatformParticle.transform.parent = transform;
    }

    void Update()
    {
        lastPressedPlatform = GameManager.lastPressedPlatform;
    }

    public void ParticleContoller()
    {
        // Проверка на паузе ли игра
        if (Time.timeScale == 0f) { return; }

        GameManager.hasTower = hasTower;
        if(Camera.transform.parent == null)
        {
            if (CanvasOfTowerUI.enabled == true)
            {
                if (lastPressedPlatform == gameObject)
                {
                    CanvasOfTowerUI.enabled = false;
                    CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
                    CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
                    CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
                    CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;
                    CanvasOfTowerUI.gameObject.transform.GetChild(4).GetComponent<Button>().enabled = false;

                    CurrentPlatformParticle.GetComponent<ParticleSystem>().Stop();
                }
                else
                {
                    if (lastPressedPlatform != null)
                    {
                        lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
                    }
                    CurrentPlatformParticle.GetComponent<ParticleSystem>().Play();

                    if (GetComponent<PlatformController>().hasTower)
                    {
                        CanvasOfTowerUI.gameObject.transform.GetChild(5).gameObject.SetActive(true);
                        CanvasOfTowerUI.gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = transform.GetChild(1).GetComponent<TowerController>().level.ToString();
                    }
                    else
                    {
                        CanvasOfTowerUI.gameObject.transform.GetChild(5).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                CanvasOfTowerUI.enabled = true;
                CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = true;
                CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = true;
                CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = true;
                CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = true;
                CanvasOfTowerUI.gameObject.transform.GetChild(4).GetComponent<Button>().enabled = true;
                CurrentPlatformParticle.GetComponent<ParticleSystem>().Play();

                if (hasTower)
                {
                    CanvasOfTowerUI.gameObject.transform.GetChild(5).gameObject.SetActive(true);
                    CanvasOfTowerUI.gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = transform.GetChild(1).GetComponent<TowerController>().level.ToString();
                }
                else
                {
                    CanvasOfTowerUI.gameObject.transform.GetChild(5).gameObject.SetActive(false);
                }
            }

            GameManager.lastPressedPlatform = gameObject;
        }
    }
}
