using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 5;

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
            Destroy(this.gameObject);
        }
    }
}
