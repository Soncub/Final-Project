using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public float health;
    public float maxHealth;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-damage);

        }
    }

    public void ChangeHealth(float amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        Debug.Log("Enemy Health: " + health + "/" + maxHealth);
    }
}
