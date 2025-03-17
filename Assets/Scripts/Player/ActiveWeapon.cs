using Cinemachine;
using StarterAssets;
using System.Collections;
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

    public MenuSettings menuSettings;

    WeaponSO currentWeaponSO;
    StarterAssetsInputs starterAssetsInputs;
    FirstPersonController firstPersonController;
    Weapon currentWeapon;
    Animator animator;
    AudioManager audioManager;



    const string SHOOT_STRING_PISTOL = "Pistol";
    const string SHOOT_STRING_MASHINE_GUN = "MachineGun";
    const string SHOOT_STRING_SNIPER_RIFLE = "SniperRifle";


    float timeSinceLastShot = 0f;
    float defaultFOV;
    float defaultRotationSpeed;
    int currentAmmo;
    bool isSniperRifle;
    bool wasZoomed = false;
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
        audioManager = FindFirstObjectByType<AudioManager>();
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
        defaultMoveSpeed = firstPersonController.MoveSpeed;
        defaultSprintSpeed = firstPersonController.SprintSpeed;
    }

    void Update()
    {
        HandleShoot();
        HandleZoom();
        PlayZoomFX();
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

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0 && !menuSettings.isPaused)
        {
            currentWeapon.Shoot(currentWeaponSO, isSniperRifle);
            SwitchAnimationWeapon(currentWeaponSO);
            WeaponSound(currentWeaponSO);
            StartCoroutine(PlayBulletDroppedRoutine());
            timeSinceLastShot = 0;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetsInputs.ShootInput(false);
        }
    }

    void SwitchAnimationWeapon(WeaponSO weapon)
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

    void WeaponSound(WeaponSO weapon)
    {
        if (weapon.IsSniperRifle)
        {
            audioManager.PlaySound("SniperRifleShoot", 0.8f);
        }
        else if (weapon.IsMashineGun)
        {
            audioManager.PlaySound("MachineGun");
        }
        else
        {
            audioManager.PlaySound("MachineGun");
        }

    }

    IEnumerator PlayBulletDroppedRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        audioManager.PlaySound("bulletDropped", 1.5f);
    }

    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetsInputs.zoom && !menuSettings.isPaused)
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
            if (!menuSettings.isPaused)
            {
                crosshair.SetActive(true);
            }

            playerFollowCamera.m_Lens.FieldOfView = defaultFOV;
            weaponCamera.fieldOfView = defaultFOV;

            DefaultMoveSpeed();

            zoomVignette.SetActive(false);
            currentWeaponSO.OnZoom = false;
            firstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
        }
    }

    void PlayZoomFX()
    {
        if (currentWeaponSO.OnZoom && !wasZoomed)
        {
            audioManager.PlaySound("ZoomOn");
        }
        else if (!currentWeaponSO.OnZoom && wasZoomed)
        {
            audioManager.PlaySound("ZoomOff");
        }
        wasZoomed = currentWeaponSO.OnZoom;
    }

    void DefaultMoveSpeed()
    {
        firstPersonController.MoveSpeed = defaultMoveSpeed;
        firstPersonController.SprintSpeed = defaultSprintSpeed;
    }
}
