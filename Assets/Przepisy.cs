using System.Collections;
using UnityEngine;

public class Przepisy : MonoBehaviour
{
    public GameObject cofnij_button;
    public GameObject next_button;
    public GameObject pierwsza_strona;
    public GameObject druga_strona;
    public FirstPersonLook firstPersonLookScript;
    public FirstPersonMovement firstPersonMovement;
    public PauseMenu pauseMenu;
    private bool cooldownActive = false;
    private float cooldownDuration = 0.25f;
    public bool KsiazkaWidoczna = false;


    private void Start()
    {

    }

    public void WlaczKsiazke()
    {
        firstPersonMovement.enabled = false;
        pauseMenu.enabled = false;
        firstPersonLookScript.enabled = false;
        Debug.Log("Wlaczam Ksiazke!");
        pierwsza_strona.SetActive(true);
        next_button.SetActive(true);
        cofnij_button.SetActive(false);
        druga_strona.SetActive(false);

        Cursor.lockState = CursorLockMode.Confined;
    }

    public void WylaczKsiazke()
    {
        firstPersonMovement.enabled = true;
        pauseMenu.enabled = true;
        firstPersonLookScript.enabled = true;
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
        if (KsiazkaWidoczna == false)
        {
            if (Input.GetKeyDown(KeyCode.F) && !cooldownActive)
            {
                WlaczKsiazke();
                KsiazkaWidoczna = true;
                StartCooldown();
            }
        }

    }

    private void Update()
    {
        if (KsiazkaWidoczna == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && !cooldownActive)
            {
                WylaczKsiazke();
                KsiazkaWidoczna = false;
                StartCooldown();
            }
        }
    }

    private void StartCooldown()
    {
        cooldownActive = true;
        StartCoroutine(ResetCooldown());
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        cooldownActive = false;
    }

    private void OnMouseEnter()
    {
        GetComponent<Outline>().enabled = true;
    }
    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }
}
