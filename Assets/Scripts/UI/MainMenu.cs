using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button _startGame;
    [SerializeField] Button _quitGame;
    [SerializeField] Button _loadtutorial;

    // Start is called before the first frame update
    void Start()
    {
        _startGame.onClick.AddListener(StartGame);
        _quitGame.onClick.AddListener(QuitGame);
        _loadtutorial.onClick.AddListener(LoadTutorial);
    }

    // Update is called once per frame
    private void StartGame()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
    }

    private void QuitGame()
    {
        ScenesManager.Instance.QuitGame();
    }
    private void LoadTutorial()
    {
        ScenesManager.Instance.LoadHowToPlay();
    }
}
