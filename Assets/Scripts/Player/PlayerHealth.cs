using Cinemachine;
using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int health = 5;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    [SerializeField] GameObject lowHPImage;


    int currentHealth;
    int gameOverVirtualCameraPriority = 20;

    public bool isALive;

    public GameManager gameManager;

    private void Awake()
    {
        currentHealth = health;
        AdjustShieldUI();
        isALive = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        LowHPImageDraw();
        AdjustShieldUI();

        if (currentHealth <= 0 && gameManager.enemiesLeft > 0)
        {
            isALive = false;
            PlayerGameOver();
        }
    }

    void LowHPImageDraw()
    {
        Debug.Log("I am LowHPImage");
        if (currentHealth <= 3 && isALive)
        {
            Debug.Log("If on worcking!");
            lowHPImage.SetActive(true);
        }
        else if (currentHealth >= 4 && isALive)
        {
            lowHPImage.SetActive(false);
        }
        else
        {
            lowHPImage.SetActive(false);
        }
    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        lowHPImage.SetActive(false);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
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
