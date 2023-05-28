using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public int level;
    public void LoadMak()
    {
        SceneManager.LoadScene("TutorialMak");
    }
    public void LoadKafelek()
    {
        SceneManager.LoadScene("TutorialKafelek");
    }
}
