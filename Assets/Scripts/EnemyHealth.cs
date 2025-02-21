using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject robotExplosionVFX;

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
            Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
