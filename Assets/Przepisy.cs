using UnityEngine;

public class Przepisy : MonoBehaviour
{
    public GameObject cofnij_button;
    public GameObject next_button;
    public GameObject pierwsza_strona;
    public GameObject druga_strona;
    public bool KsiazkaWidoczna = false;



    public void WlaczKsiazke()
    {
        pierwsza_strona.SetActive(true);
        next_button.SetActive(true);
        cofnij_button.SetActive(false);
        druga_strona.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void WylaczKsiazke()
    {
        pierwsza_strona.SetActive(false);
        next_button.SetActive(false);
        cofnij_button.SetActive(false);
        druga_strona.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void NastepnaStrona()
    {
        pierwsza_strona.SetActive(false);
        druga_strona.SetActive(true);
        cofnij_button.SetActive(true);
        next_button.SetActive(false);
    }

    public void PoprzedniaStrona()
    {
        pierwsza_strona.SetActive(true);
        druga_strona.SetActive(false);
        cofnij_button.SetActive(false);
        next_button.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (!KsiazkaWidoczna)
            if (Input.GetKeyDown(KeyCode.F))
            {
                WlaczKsiazke();
                KsiazkaWidoczna = true;
            }
    }

    private void Update()
    {
        if (KsiazkaWidoczna)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                WylaczKsiazke();
                KsiazkaWidoczna = false;
            }
    }

}
