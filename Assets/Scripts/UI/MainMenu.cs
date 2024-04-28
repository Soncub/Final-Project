using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioClip triggerclip;

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
        Audio.Instance.PlaySound(triggerclip);
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
    }

    private void QuitGame()
    {
        Audio.Instance.PlaySound(triggerclip);
        ScenesManager.Instance.QuitGame();
    }
    private void LoadTutorial()
    {
        Audio.Instance.PlaySound(triggerclip);
        ScenesManager.Instance.LoadHowToPlay();
    }
}
