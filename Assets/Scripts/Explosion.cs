using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;

    void Start()
    {
        Explode();
    }

    void Explode()
    {
        //do Damage to Player
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
