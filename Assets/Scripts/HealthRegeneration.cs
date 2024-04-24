using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegeneration : MonoBehaviour
{
    public PlayerController currentHealth;
    public PlayerController maxHealth;
    public float pointIncreasePerSecond;
    // Start is called before the first frame update
    void Start()
    {
        pointIncreasePerSecond = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth += pointIncreasePerSecond * Time.deltaTime;

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
