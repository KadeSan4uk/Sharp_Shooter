using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;
    public bool IsPistol = false;
    public bool IsMashineGun = false;
    public bool IsSniperRifle = false;
    public int Damage = 1;
    public float FireRate = 0.5f;
    public GameObject HitVFXPrefab;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public bool OnZoom = false;
    public float ZoomAmount = 10f;   
    public int MagazineSize = 12;
}
