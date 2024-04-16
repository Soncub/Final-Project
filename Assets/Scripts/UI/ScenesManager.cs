using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        MainMenu,
        StartMenu,
        BattleScreen,
        HowToPlay
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadBattleScreen()
    {
        SceneManager.LoadScene(Scene.BattleScreen.ToString());
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
    public void LoadStart()
    {
        SceneManager.LoadScene(Scene.StartMenu.ToString());
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene(Scene.HowToPlay.ToString());
    }

}

