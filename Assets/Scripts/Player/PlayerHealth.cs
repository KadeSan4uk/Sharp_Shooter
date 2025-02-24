using Cinemachine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;

    int currentHealth;
    int gameOverVirtualCameraPriority = 20;

    private void Awake()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
            Destroy(this.gameObject);
        }
    }
}
