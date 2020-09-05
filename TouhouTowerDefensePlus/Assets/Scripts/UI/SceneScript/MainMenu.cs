using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{



    void Awake()
    {

        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        AudioManager.instance.PlayMusic("bgm02");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ManualScene()
    {
        SceneManager.LoadScene("Manual");

    }
    public void TutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
