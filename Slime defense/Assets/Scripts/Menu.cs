using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void Play() 
    {
        SceneManager.LoadScene(1);
    }
    public void Exit() 
    {
        Application.Quit();
    }
}
