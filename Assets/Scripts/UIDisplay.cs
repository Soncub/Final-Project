using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    public static UIDisplay instance;
    //accessing the PlayerController script
    public GameObject Player;
    public PlayerController playercontroller;

    public Text scoreText;
    public Text healthText;

    int score = 0;
    public float health = 0.0f;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playercontroller = Player.GetComponent<PlayerController>();
        health = playercontroller.maxHealth;
        scoreText.text = "Score " + score.ToString();
        healthText.text = "Health " + health.ToString();
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "SCORE " + score.ToString();
    }

    public void UpdateHealth(float amount)
    {
        health = health + amount;
        healthText.text = "Health " + health.ToString();
    }
}