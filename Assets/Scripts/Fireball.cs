using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damage = 1;
    public float splashRange = 5;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (splashRange > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyDamage>();
                if (enemy != null)
                {
                    enemy.ChangeHealth(-damage);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            var enemy = other.GetComponent<Collider>().GetComponent<EnemyDamage>();
            if (enemy != null)
            {
                enemy.ChangeHealth(-damage);
                Destroy(gameObject);
            }
        }
    }
}
