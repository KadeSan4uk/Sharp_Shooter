using Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] LayerMask interactionLayers;

    CinemachineImpulseSource impulsSource;

    private void Awake()
    {
        impulsSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Shoot(WeaponSO weaponSO, bool IsSniperRifle)
    {
        RaycastHit hit;
        muzzleFlash.Play();
        impulsSource.GenerateImpulse();

        if (IsSniperRifle && !weaponSO.OnZoom)
        {
            float scatterAmount = Random.Range(0.1f, 0.2f);
            Vector3 randomScatterSniper = Camera.main.transform.forward * scatterAmount + new Vector3(Random.Range(-0.01f, 0.02f), Random.Range(-0.01f, 0.02f), 0);

            if (Physics.Raycast(Camera.main.transform.position, randomScatterSniper, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
            {
                Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
                EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
                enemyHealth?.TakeDamage(weaponSO.Damage);
            }
        }
        else
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore))
            {
                Instantiate(weaponSO.HitVFXPrefab, hit.point, Quaternion.identity);
                EnemyHealth enemyHealth = hit.collider.GetComponentInParent<EnemyHealth>();
                enemyHealth?.TakeDamage(weaponSO.Damage);
            }
        }
    }
}
