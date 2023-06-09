using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject SettingsMenu;

    public void Play() 
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        Debug.Log("Меню настроек");
        SettingsMenu.SetActive(true);
    }

    public void Exit() 
    {
        Debug.Log("Выход из игры!");
        Application.Quit();
    }
}
