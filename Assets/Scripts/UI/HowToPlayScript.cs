using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayScript : MonoBehaviour
{
    [SerializeField] Button _startGame;

    // Start is called before the first frame update
    void Start()
    {
        _startGame.onClick.AddListener(StartMenu);
    }

    // Update is called once per frame
    private void StartMenu()
    {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.StartMenu);
    }
}