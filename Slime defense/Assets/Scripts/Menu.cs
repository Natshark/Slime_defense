using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject SettingsMenu;

    public bool isSettings;

    void Start()
    {
        Time.timeScale = 1.0f;
        isSettings = false;
    }

    public void Play() 
    {
        SceneManager.LoadScene(1);
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

    public void Exit() 
    {
        Application.Quit();
    }
}
