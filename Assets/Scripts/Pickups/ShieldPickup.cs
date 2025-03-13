using Unity.VisualScripting;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 80f;
    [SerializeField] int ShieldUp = 10;

    const string PLAYER_STRING = "Player";

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.AdjustShieldPickup(ShieldUp);
            audioManager.PlaySound("shieldUp");
            Destroy(this.gameObject);
        }
    }
}
