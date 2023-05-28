using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZmianaSlajdow : MonoBehaviour
{
    public GameObject nastepnyButton;
    public Image obrazki;
    public GameObject poprzedniButton;
    public GameObject grajButton;
    int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TutorialKafelek")
        {
            if (i > 1 && i < 7)
            {
                poprzedniButton.SetActive(true);
            }
            else
            {
                poprzedniButton.SetActive(false);
            }

            if (i >= 1 && i < 6)
            {
                nastepnyButton.SetActive(true);
                grajButton.SetActive(false);
            }
            else
            {
                nastepnyButton.SetActive(false);
                grajButton.SetActive(true);
            }
        }
        else
        {
            if (i > 1 && i < 8)
            {
                poprzedniButton.SetActive(true);
            }
            else
            {
                poprzedniButton.SetActive(false);
            }

            if (i >= 1 && i < 7)
            {
                nastepnyButton.SetActive(true);
                grajButton.SetActive(false);
            }
            else
            {
                nastepnyButton.SetActive(false);
                grajButton.SetActive(true);
            }
        }

    }

    public void NastepnyObrazek()
    {
        if (SceneManager.GetActiveScene().name == "TutorialKafelek")
        {
            if (i < 6)
            {
                i++;
                string nazwa = "Kafelek_tut_" + i.ToString();
                Debug.Log(nazwa);
                obrazki.sprite = Resources.Load<Sprite>(nazwa);
            }
        }
        else
        {
            if (i < 7)
            {
                i++;
                string nazwa = "tutorial_mak_" + i.ToString();
                Debug.Log(nazwa);
                obrazki.sprite = Resources.Load<Sprite>(nazwa);
            }
        }

    }

    public void PoprzedniObrazek()
    {
        if (SceneManager.GetActiveScene().name == "TutorialKafelek")
        {
            if (i > 1)
            {
                i--;
                string nazwa = "Kafelek_tut_" + i.ToString();
                Debug.Log(nazwa);
                obrazki.sprite = Resources.Load<Sprite>(nazwa);
            }
        }
        else
        {
            if (i > 1)
            {
                i--;
                string nazwa = "tutorial_mak_" + i.ToString();
                Debug.Log(nazwa);
                obrazki.sprite = Resources.Load<Sprite>(nazwa);
            }
        }
    }

    public void Graj()
    {
        if (SceneManager.GetActiveScene().name == "TutorialKafelek")
        {
            SceneManager.LoadScene("Kafelek");
        }
        else
        {
            SceneManager.LoadScene("ScenaMakowa");
        }
    }
}
