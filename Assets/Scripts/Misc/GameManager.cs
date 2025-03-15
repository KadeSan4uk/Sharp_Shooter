using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinTextContainer;
    [SerializeField] GameObject enemiesTextContainer;
    [SerializeField] TMP_Text[] enemiesKilledText;
    [SerializeField] Transform weaponCamera;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;

    int gameOverVirtualCameraPriority = 20;


    public int enemiesLeft = 0;
    int enemiesKilled = 0;

    const string ENEMIES_LEFT_STRING = "Enemies left: ";
    const string ENEMIRS_KILLED = "Enemies killed: ";

    public bool isAWinUI = false;

    public PlayerHealth playerHealth;
    public GameObject lowHPImage;

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0)
        {
            isAWinUI = true;
            YouWinUI();
        }
    }

    public void AdjustEnemiesKilled(int amount)
    {
        enemiesKilled += amount;
        foreach (var item in enemiesKilledText)
        {
            item.text = ENEMIRS_KILLED + enemiesKilled.ToString();
        }
    }

    public void RestartButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitButtom()
    {
        Debug.LogWarning("Does not work in the Unity Editor!");
        Application.Quit();
    }

    void YouWinUI()
    {
        playerHealth.isALive = false;
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        youWinTextContainer.SetActive(true);
        enemiesTextContainer.SetActive(false);
        lowHPImage.SetActive(false);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
        Destroy(playerHealth.gameObject);
    }
}
