using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KoniecGryPunkty : MonoBehaviour
{
    public GameObject panel_pauzy;
    public GameObject panel_ksiazki;
    void Start()
    {
        transform.Find("Punkty").gameObject.GetComponent<TMP_Text>().text = "Twoje punkty to: " + Convert.ToInt32(punkty.score);
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }
}
