using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegeneration : MonoBehaviour
{
    public int pointIncreasePerSecond;
    public float currentHealth;
    public float maxHealth;

    void Start()
    {
        pointIncreasePerSecond = 1;
    }

    void Update()
    {
        //this.GetComponent<PlayerController>().currentHealth;
        //this.GetComponent<PlayerController>().maxHealth;
        currentHealth += pointIncreasePerSecond * Time.deltaTime;

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
