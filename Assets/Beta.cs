using UnityEngine;
using UnityEngine.SceneManagement;

public class Beta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextScene", 5f);
    }

    // Update is called once per frame
    public void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
