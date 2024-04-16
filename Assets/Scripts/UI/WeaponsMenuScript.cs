using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsMenuScript : MonoBehaviour
{
    [SerializeField] Button _startGame;
    [SerializeField] Button _loadtutorial;

    // Start is called before the first frame update
    void Start()
    {
        _startGame.onClick.AddListener(StartGame);
        _loadtutorial.onClick.AddListener(LoadTutorial);
    }

    // Update is called once per frame
    private void StartGame()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.StartMenu);
    }
    private void LoadTutorial()
    {
        ScenesManager.Instance.LoadBattleScreen();
    }
}