using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] int health = 3;

    int currentHealth;

    private void Awake()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
