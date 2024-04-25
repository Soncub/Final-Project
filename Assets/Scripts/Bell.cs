using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    public float damage = 5;

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyDamage enemy = other.GetComponent<EnemyDamage>();

        if (enemy != null)
        {
            enemy.ChangeHealth(-damage);
        }
    }
}
