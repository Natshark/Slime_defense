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
        Debug.Log("���� ��������");
        SettingsMenu.SetActive(true);
    }

    public void Exit() 
    {
        Debug.Log("����� �� ����!");
        Application.Quit();
    }
}
