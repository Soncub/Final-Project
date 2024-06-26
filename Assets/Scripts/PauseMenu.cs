using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public AudioClip triggerclip;
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button _startGame;
    public UpgradeMenu UpgradeMenu;

    void Start()
    {
        _startGame.onClick.AddListener(StartGame);
    }
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) && UpgradeMenu.isMenuActive == false)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void StartGame()
    {
        Audio.Instance.PlaySound(triggerclip);
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.StartMenu);
    }
}
