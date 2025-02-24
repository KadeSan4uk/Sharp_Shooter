using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = 2f;
    [SerializeField] int damage = 2;

    PlayerHealth player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }

    private void Update()
    {
        turretHead.LookAt(playerTargetPoint);
    }

    IEnumerator FireRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(fireRate);

            Projectile newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }
}
