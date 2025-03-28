using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;
using TMPro;

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
    public FirstPersonController firstPersonController;

    private StarterAssetsInputs starterAssetsInputs;

    public float defaultSensitivity;

    public bool isPaused = false;

    private void Awake()
    {
        starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        pauseAction.action.performed += TogglePause;
    }

    private void OnDestroy()
    {
        pauseAction.action.performed -= TogglePause;
    }

    private void Start()
    {
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
        SetSensitivity(savedSensitivity);

        AudioListener.volume = savedVolume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
        firstPersonController.RotationSpeed = defaultSensitivity;
    }

    public void TogglePause(InputAction.CallbackContext context)
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
        firstPersonController.RotationSpeed = value;
        defaultSensitivity = value;
        PlayerPrefs.Save();
    }
}
