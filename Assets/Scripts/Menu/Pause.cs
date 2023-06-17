using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public FirstPersonLook firstPersonLookScript; // Referencja do skryptu "First Person Look"
    private bool isPaused = false;
    private bool SterowaniePokazywane = false;
    private GameObject celownik;
    public MakeACoffe coffe;
    public MakeACoffe coffe2;
    public GameObject panel_pauzy;
    public GameObject panel_ksiazki;
    public GameObject HowToPlay_panel;
    private void Start()
    {
        celownik = GameObject.Find("Celownik");
        isPaused = false;
        SterowaniePokazywane = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (!SterowaniePokazywane)
                {
                    ResumeGame();
                }
                else
                {
                    pauseMenuUI.SetActive(true);
                    HowToPlay_panel.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    SterowaniePokazywane = false;
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        if (coffe != null)
        {
            coffe.enabled = true;
        }
        if (coffe2 != null)
        {
            coffe2.enabled = true;
        }
        celownik.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        // W³¹cz skrypt "First Person Look"
        firstPersonLookScript.enabled = true;
        HowToPlay_panel.SetActive(false);
    }

    void PauseGame()
    {
        if(coffe != null)
        {
            coffe.enabled = false;
        }
        if (coffe2 != null)
        {
            coffe2.enabled = false;
        }
        celownik.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        // Wy³¹cz skrypt "First Person Look"
        firstPersonLookScript.enabled = false;
    }

    public void Continue()
    {
        ResumeGame();
    }

    public void SetHoWtoPlayBoolFalse()
    {
        SterowaniePokazywane = false;
    }

    public void RestartLevel()
    {
        panel_pauzy.SetActive(false);
        panel_ksiazki.SetActive(false);
        punkty.score = 0;
        punkty.efficency = 0;
        GameFlow.CustomerCount = 0;
        GameFlow.spawnCount = 0;
        GameFlow.isSpawning = false;
        GameFlow.spawningRange = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Cursor.visible = false;
    }

    public void PokazSterowanie()
    {
        
        HowToPlay_panel.SetActive(true);
        panel_pauzy.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SterowaniePokazywane = true;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Nazwa sceny g³ównego menu
    }
}