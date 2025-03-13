using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    const string PLAYER_STRING = "Player";

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnAmmoPickup(activeWeapon);
            Destroy(this.gameObject);
        }
    }

    protected abstract void OnAmmoPickup(ActiveWeapon activeWeapon);
}
