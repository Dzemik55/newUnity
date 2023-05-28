using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KoniecGryPunkty : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Punkty").gameObject.GetComponent<TMP_Text>().text = "Twoje punkty to: " + Mathf.Abs(punkty.score);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
