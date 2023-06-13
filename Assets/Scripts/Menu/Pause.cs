using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public FirstPersonLook firstPersonLookScript; // Referencja do skryptu "First Person Look"
    private bool isPaused = false;
    private GameObject celownik;
    public MakeACoffe coffe;
    public MakeACoffe coffe2;
    private void Start()
    {
        celownik = GameObject.Find("Celownik");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isPaused)
            {
                ResumeGame();
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

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Nazwa sceny g³ównego menu
    }
}