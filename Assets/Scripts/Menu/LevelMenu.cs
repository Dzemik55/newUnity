using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public int level;
    public void LoadMak()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadKafelek()
    {
        SceneManager.LoadScene(2);
    }
}
