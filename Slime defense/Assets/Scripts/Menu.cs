using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int counterSlides = 0;
    public GameObject[] slides;
    public GameObject howToPlayUI;

    public Text HighScore;

    void Start()
    {
        Time.timeScale = 1.0f;

        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore.text = Convert.ToString(PlayerPrefs.GetInt("HighScore"));
        }
    }
    public void Play() 
    {
        SceneManager.LoadScene(1);
    }

    public void howToPlay() 
    {
        howToPlayUI.SetActive(true);
        slides[counterSlides].SetActive(true);
    }

    public void nextSlide()
    {
        counterSlides++;
        slides[counterSlides].SetActive(true);
        slides[counterSlides-1].SetActive(false);
    }

    public void toMenu()
    {
        slides[counterSlides].SetActive(false);
        howToPlayUI.SetActive(false);
        counterSlides = 0;
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
