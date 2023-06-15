using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject lastPressedPlatform;
    public Canvas CanvasOfTowerUI;

    public GameObject PauseUI;
    public GameObject LoseGameUI;

    public GameObject magicTower;
    public GameObject cannonTower;
    public GameObject SettingsMenu;

    public bool hasTower;
    public bool isPaused;
    public bool isSettings;

    public int PlayerMoney;
    public Text moneyText;

    public Text homeHpText;
    public int homeHp = 100;

    public int magicTowerPrice = 50;
    public int cannonTowerPrice = 30;

    void Start()
    {
        Time.timeScale = 1.0f;

        PlayerMoney = 0;
        isPaused = false;
        isSettings = false;
    }

    void Update()
    {
        moneyText.text = PlayerMoney.ToString();
        homeHpText.text = homeHp.ToString();

        if (homeHp <= 0)
        {
            GameLose();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {


            if (isPaused)
            {
                SettingsMenu.SetActive(false);
                isSettings = false;
                resume();
            }
            else 
            {
                Cursor.lockState = CursorLockMode.None;
                pause();
            }
        }
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

    public void pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
    }

    public void resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        if (Camera.main.transform.parent != null)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = true;
        CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = true;
    }

    public void menu() 
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        if (!isSettings)
        {
            SettingsMenu.SetActive(true);
            isSettings = true;
        }
        else
        {
            SettingsMenu.SetActive(false);
            isSettings = false;
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void GameLose()
    {
        Time.timeScale = 0.0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;

        LoseGameUI.SetActive(true);
    }
}
