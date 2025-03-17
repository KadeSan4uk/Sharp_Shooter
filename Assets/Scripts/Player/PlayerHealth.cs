using Cinemachine;
using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int health = 10;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    [SerializeField] GameObject lowHPImage;


    public int currentHealth;
    int gameOverVirtualCameraPriority = 20;

    public bool isALive;
    public bool isTakeDamage = false;

    public GameManager gameManager;
    public AudioManager audioManager;
    public HeartBeats heartBeats;

    private void Awake()
    {
        currentHealth = health;
        currentHealth = 5;
        AdjustShieldUI();
        isALive = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        isTakeDamage = true;

        PlaySoundTakeDamage();
        LowHPImageDraw();
        AdjustShieldUI();

        if (currentHealth <= 0 && gameManager.enemiesLeft > 0)
        {
            isALive = false;
            PlayerGameOver();
            PlaySoundTakeDamage();
        }
        isTakeDamage = false;
    }

    void PlaySoundTakeDamage()
    {
        if (isTakeDamage && isALive)
        {
            audioManager.PlaySound("PlayerTakeDamage", 4f);
        }

        if (!isALive)
        {
            audioManager.PlaySound("PlayerDeath", 2f);
        }
    }

    void LowHPImageDraw()
    {
        if (currentHealth <= 3 && isALive)
        {
            heartBeats.PlaySound();
            lowHPImage.SetActive(true);
        }
        else
        {
            heartBeats.StopPlay();
            lowHPImage.SetActive(false);
        }
    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        lowHPImage.SetActive(false);        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Destroy(this.gameObject);
    }

    void AdjustShieldUI()
    {
        for (int i = 0; i < shieldBars.Length; i++)
        {
            if (i < currentHealth)
            {
                shieldBars[i].gameObject.SetActive(true);
            }
            else
            {
                shieldBars[i].gameObject.SetActive(false);
            }
        }
    }

    public void AdjustShieldPickup(int amount)
    {
        currentHealth += amount;

        if (currentHealth >= amount)
        {
            currentHealth = health;
        }
        AdjustShieldUI();
        LowHPImageDraw();
    }
}
