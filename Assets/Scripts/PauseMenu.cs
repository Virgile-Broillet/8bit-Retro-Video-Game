using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu;


    private bool isPaused = false;
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.PauseMenu.pause.performed += ctx => TogglePause();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            PauseGame();
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Option()
    {
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void BackToPause()
    {
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Assurez-vous que le temps est rétabli
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // utile pour tester dans Unity
        Application.Quit();
    }
}