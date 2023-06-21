using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject lastPressedPlatform;
    public GameObject createdTower;
    public GameObject destroyedTower;
    public GameObject Tower;

    public Canvas CanvasOfTowerUI;

    public GameObject PauseUI;
    public GameObject LoseGameUI;

    public GameObject magicTower;
    public GameObject cannonTower;
    public GameObject teslaTower;
    public GameObject SettingsMenu;

    public bool hasTower;
    public bool isPaused;
    public bool isSettings;

    public int PlayerMoney;
    public Text moneyText;

    public Text homeHpText;
    public float homeHp = 100;

    public int magicTowerPrice = 50;
    public int cannonTowerPrice = 30;
    public int teslaTowerPrice = 80;
    public int upgradePrice = 100;

    public float userSensitivity;
    public float gameVolume;
    public CameraRotation CameraRotation;
    public GameObject AudioManager;

    public Slider SensitivitySlider;
    public Slider VolumeSlider;

    public GameObject[] CannonTowers;
    public GameObject[] MagicTowers;
    public GameObject[] TeslaTowers;
    public GameObject[] UpgradingTowers;

    void Start()
    {
        Time.timeScale = 1.0f;

        PlayerMoney = 999;
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

            createdTower = Instantiate(magicTower, lastPressedPlatform.transform.position, Quaternion.identity);
            createdTower.transform.parent = lastPressedPlatform.transform;
            lastPressedPlatform.GetComponent<PlatformController>().hasTower = true;
            CanvasOfTowerUI.enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;
            lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }

    public void createTeslaTower()
    {
        if (!hasTower && PlayerMoney >= teslaTowerPrice)
        {
            PlayerMoney -= teslaTowerPrice;

            createdTower = Instantiate(teslaTower, lastPressedPlatform.transform.position, Quaternion.identity);
            createdTower.transform.parent = lastPressedPlatform.transform;
            lastPressedPlatform.GetComponent<PlatformController>().hasTower = true;
            CanvasOfTowerUI.enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;
            lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }
    public void createCannonTower()
    {
        if (!hasTower && PlayerMoney >= cannonTowerPrice)
        {
            PlayerMoney -= cannonTowerPrice;

            createdTower = Instantiate(cannonTower, lastPressedPlatform.transform.position, Quaternion.identity);
            createdTower.transform.parent = lastPressedPlatform.transform;
            lastPressedPlatform.GetComponent<PlatformController>().hasTower = true;
            CanvasOfTowerUI.enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;
            lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }

    public void upgradeTower()
    {
        if (hasTower && PlayerMoney >= upgradePrice)
        {
            Tower = lastPressedPlatform.transform.GetChild(1).gameObject;
            if (Tower.GetComponent<TowerController>().level != 3)
            {
                if (Tower.CompareTag("TeslaTower"))
                {
                    UpgradingTowers = TeslaTowers;
                }

                createdTower = Instantiate(UpgradingTowers[Tower.GetComponent<TowerController>().level], Tower.transform.position, Quaternion.identity);
                createdTower.transform.parent = lastPressedPlatform.transform;
                Destroy(Tower);
                PlayerMoney -= upgradePrice;

                CanvasOfTowerUI.enabled = false;
                CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
                CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
                CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
                CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;
                lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    public void destroyTower()
    {
        if (hasTower)
        {
            destroyedTower = lastPressedPlatform.transform.GetChild(1).gameObject;

            if (destroyedTower.CompareTag("MagicTower"))
            {
                PlayerMoney += magicTowerPrice / 4;
            }
            else if (destroyedTower.CompareTag("CannonTower"))
            {
                PlayerMoney += cannonTowerPrice / 4;
            } 
            else if (destroyedTower.CompareTag("TeslaTower")) 
            {
                PlayerMoney += teslaTowerPrice / 4;
            }

            Destroy(destroyedTower);
            lastPressedPlatform.GetComponent<PlatformController>().hasTower = false;
            hasTower = false;
            CanvasOfTowerUI.enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
            CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;
            lastPressedPlatform.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }
    }

    public void pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Camera.main.GetComponent<AudioListener>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;
    }

    public void resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        Camera.main.GetComponent<AudioListener>().enabled = true;

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
        CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = true;
        CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = true;
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

            if (PlayerPrefs.HasKey("UserSens"))
            {
                SensitivitySlider.value = PlayerPrefs.GetFloat("UserSens");
            }
            else
            {
                SensitivitySlider.value = 1.0f;
            }

            if (PlayerPrefs.HasKey("GameVolume"))
            {
                VolumeSlider.value = PlayerPrefs.GetFloat("GameVolume");
            }
            else
            {
                VolumeSlider.value = 1.0f;
            }
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
        Camera.main.GetComponent<AudioListener>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CanvasOfTowerUI.gameObject.transform.GetChild(0).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(1).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(2).GetComponent<Button>().enabled = false;
        CanvasOfTowerUI.gameObject.transform.GetChild(3).GetComponent<Button>().enabled = false;

        LoseGameUI.SetActive(true);
    }
    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("UserSens", SensitivitySlider.value);
        PlayerPrefs.SetFloat("GameVolume", VolumeSlider.value);
        Camera.main.GetComponent<CameraRotation>().sensetivityMouse = PlayerPrefs.GetFloat("UserSens");
        AudioListener.volume = PlayerPrefs.GetFloat("GameVolume");
    }
}
