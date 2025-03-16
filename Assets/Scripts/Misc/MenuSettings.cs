using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;

public class MenuSettings : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject popupMainMenu;
    public GameObject popupOptions;
    public GameObject crosshair;

    [Header("Sliders")]
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    [Header("Input")]
    public InputActionReference pauseAction;

    private StarterAssetsInputs starterAssetsInputs;

    public bool isPaused = false;

    private void Awake()
    {
        starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        pauseAction.action.performed += _ => TogglePause();
    }
    private void OnDestroy()
    {
        pauseAction.action.performed -= _ => TogglePause();
    }

    private void Start()
    {
        OpenMainMenu();
        CheckPlayerPrefs();
    }

    private void CheckPlayerPrefs()
    {
        OpenMainMenu();

        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1.0f);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetFloat("Sensitivity", 1.0f);
            PlayerPrefs.Save();
        }

        float savedVolume = PlayerPrefs.GetFloat("Volume", 1.0f);
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 1.0f);

        volumeSlider.value = savedVolume;
        sensitivitySlider.value = savedSensitivity;

        AudioListener.volume = savedVolume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }

    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else OpenMainMenu();
    }

    public void OpenMainMenu()
    {
        popupMainMenu.SetActive(true);
        crosshair.SetActive(false);
        popupOptions.SetActive(false);
        PauseGame();
    }

    public void OpenOptions()
    {
        popupMainMenu.SetActive(false);
        popupOptions.SetActive(true);
    }

    public void CloseOptions()
    {
        popupOptions.SetActive(false);
        popupMainMenu.SetActive(true);
    }

    public void StartGame()
    {
        ResumeGame();
        popupMainMenu.SetActive(false);
        PlayerPrefs.Save();
    }

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        starterAssetsInputs.shoot = false;
        starterAssetsInputs.move = Vector2.zero;
        starterAssetsInputs.look = Vector2.zero;
        starterAssetsInputs.cursorInputForLook = false;
        starterAssetsInputs.cursorLocked = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        popupMainMenu.SetActive(false);
        popupOptions.SetActive(false);
        crosshair.SetActive(true);
        starterAssetsInputs.shoot = false;
        starterAssetsInputs.move = Vector2.zero;
        starterAssetsInputs.look = Vector2.zero;
        starterAssetsInputs.cursorInputForLook = true;
        starterAssetsInputs.cursorLocked = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
        PlayerPrefs.Save();
    }

    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
        PlayerPrefs.Save();
    }
}
