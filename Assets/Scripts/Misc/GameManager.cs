using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;
    [SerializeField] GameObject youWinText;
    [SerializeField] GameObject enemiesTextContainer;
    [SerializeField] TMP_Text[] enemiesKilledText;

    int enemiesLeft = 0;
    int enemiesKilled = 0;

    const string ENEMIES_LEFT_STRING = "Enemies left: ";
    const string ENEMIRS_KILLED = "Enemies killed: ";

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();

        if (enemiesLeft <= 0)
        {
            youWinText.SetActive(true);
            enemiesTextContainer.SetActive(false);
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
}
