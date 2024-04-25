using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegeneration : MonoBehaviour
{
    public PlayerController playerHealth;
    public int pointIncreasePerSecond;

    void Start()
    {
        playerHealth = this.GetComponent<PlayerController>();
        pointIncreasePerSecond = 1;
    }

    void Update()
    {
        playerHealth.currentHealth += pointIncreasePerSecond * Time.deltaTime;

        if (playerHealth.currentHealth >= playerHealth.maxHealth)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
        }
    }
}
