using Cinemachine;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] GameObject crosshair;
    [SerializeField] TMP_Text ammoText;

    WeaponSO currentWeaponSO;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Weapon currentWeapon;
    Animator animator;


    const string SHOOT_STRING_PISTOL = "Pistol";
    const string SHOOT_STRING_MASHINE_GUN = "MachineGun";
    const string SHOOT_STRING_SNIPER_RIFLE = "SniperRifle";


    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;
    bool isSniperRifle;
    float defaultMoveSpeed;
    float defaultSprintSpeed;

    private void Awake()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        animator = GetComponent<Animator>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }

    private void Start()
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
        defaultMoveSpeed = firstPersonController.MoveSpeed;
        defaultSprintSpeed = firstPersonController.SprintSpeed;
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > currentWeaponSO.MagazineSize)
        {
            currentAmmo = currentWeaponSO.MagazineSize;
        }
        ammoText.text = currentAmmo.ToString("D2");
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (starterAssetsInputs.zoom)
        {
            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;

            zoomVignette.SetActive(false);
            currentWeaponSO.OnZoom = false;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }

        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }

    void HandleShoot()
    {
        isSniperRifle = currentWeaponSO.IsSniperRifle;

        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetsInputs.shoot) return;

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO, isSniperRifle);
            SwitchAnimationShoot(currentWeaponSO);
            timeSinceLastShot = 0;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void SwitchAnimationShoot(WeaponSO weapon)
    {
        if (weapon.IsSniperRifle)
        {
            animator.Play(SHOOT_STRING_SNIPER_RIFLE, 0, 0f);
        }
        else if (weapon.IsMashineGun)
        {
            animator.Play(SHOOT_STRING_MASHINE_GUN, 0, 0f);
        }
        else
        {
            animator.Play(SHOOT_STRING_PISTOL, 0, 0f);
        }
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;
        if (starterAssetsInputs.zoom)
        {
            crosshair.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;

            firstPersonController.ChangeMoveSpeedOnZoom();

            zoomVignette.SetActive(true);
            currentWeaponSO.OnZoom = true;
            firstPersonController.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            crosshair.SetActive(true);

            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;

            DefaultMoveSpeed();

            zoomVignette.SetActive(false);
            currentWeaponSO.OnZoom = false;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }

    void DefaultMoveSpeed()
    {
        firstPersonController.MoveSpeed = defaultMoveSpeed;
        firstPersonController.SprintSpeed = defaultSprintSpeed;
    }
}
