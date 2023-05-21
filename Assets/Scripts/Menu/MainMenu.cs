using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isStart;
    public bool isBack;
    public bool isLvl1;
    public bool isQuit;
    public bool isOptions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseUp()
    {
        if (isStart)
        {
            Camera.main.transform.position = new Vector3(25,8,0);
        }
        if (isOptions)
        {
            Camera.main.transform.position = new Vector3(50, 8, 0);
        }
        if (isLvl1)
        {
            SceneManager.LoadScene(1);
        }
        if (isBack)
        {
            Camera.main.transform.position = new Vector3(0, 8, 0);
        }
        if (isQuit)
        {
            Application.Quit();
            
        }
    }
}
