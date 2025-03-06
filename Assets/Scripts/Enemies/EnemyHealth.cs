using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] GameObject robotExplosionVFX;
    [SerializeField] int health = 3;

    int currentHealth;

    GameManager gameManager;
    AudioManager audioManager;

    private void Awake()
    {
        currentHealth = health;
    }

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        audioManager = FindFirstObjectByType<AudioManager>();
        gameManager.AdjustEnemiesLeft(1);
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
        audioManager.DeathRobot(0.4f);
        Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);
        gameManager.AdjustEnemiesLeft(-1);
        gameManager.AdjustEnemiesKilled(1);
        Destroy(this.gameObject);
    }
}
