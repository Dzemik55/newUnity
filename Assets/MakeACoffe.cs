using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeACoffe : MonoBehaviour
{
    public GameObject panel;
    bool isPanelActive = false;
    public Slider cukierSlider;
    public Slider mlekoSlider;
    public GameObject KawkaObject;
    private GameObject ostatnioUtworzonyObiekt;

    FirstPersonLook firstPersonLook;

    public string Kawa = "";
    public int iloscCukru = 0;
    public int iloscMleka = 0;

    private void Start()
    {
        firstPersonLook = GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>();
        Kawa = "";
        iloscCukru = 0;
        iloscMleka = 0;
    }

    public void WylaczPanel()
    {
        if (panel != null)
        {
            if (isPanelActive)
            {
                Cursor.lockState = CursorLockMode.Locked;
                firstPersonLook.canRotate = true;
                panel.GetComponent<Image>().enabled = false;
                isPanelActive = false;
                Transform[] children = panel.GetComponentsInChildren<Transform>(true);

                foreach (Transform child in children)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void WlaczPanel()
    {
        if (panel != null)
        {
            if (!isPanelActive)
            {
                Cursor.lockState = CursorLockMode.Confined;
                firstPersonLook.canRotate = false;
                panel.GetComponent<Image>().enabled = true;
                isPanelActive = true;
                Transform[] children = panel.GetComponentsInChildren<Transform>(true);

                foreach (Transform child in children)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnMouseDown()
    {
        WlaczPanel();
    }

    public void DodajCukier()
    {
        cukierSlider.value += 0.2f;
        iloscCukru += 1;
        Debug.Log(iloscCukru);
    }

    public void OdejmikCukier()
    {
        cukierSlider.value -= 0.2f;
        iloscCukru -= 1;
        Debug.Log(iloscCukru);
    }

    public void DodajMleko()
    {
        mlekoSlider.value += 0.2f;
        iloscMleka += 1;
        Debug.Log(iloscMleka);
    }

    public void OdejmikMleko()
    {
        mlekoSlider.value -= 0.2f;
        iloscMleka -= 1;
        Debug.Log(iloscMleka);
    }

    public void WybierzKawe()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        if (buttonName == "Americano_button")
        {
            Kawa = "Americano";
        }
        if (buttonName == "Latee_button")
        {
            Kawa = "Latte";
        }
        if (buttonName == "Mocha_button")
        {
            Kawa = "Mocha";
        }
        Debug.Log(Kawa);
    }

    public void ZrobKawke()
    {
        Vector3 positionToCheck = new Vector3(2.95499992f, 1.46099997f, -4.29699993f);

        // Check for objects with a Kawka component at the given position
        Kawka kawkaObject = null;
        Collider[] colliders = Physics.OverlapSphere(positionToCheck, 0.1f);
        foreach (Collider collider in colliders)
        {
            kawkaObject = collider.GetComponent<Kawka>();
            if (kawkaObject != null)
            {
                Debug.Log("Najpierw zabierz kawê która stoi obok!");
                break;
            }

            else
            {
                // There is no Kawka object at the given position
                ostatnioUtworzonyObiekt = Instantiate(KawkaObject, positionToCheck, Quaternion.identity);
                ostatnioUtworzonyObiekt.GetComponent<Kawka>().Kawa = Kawa;
                ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscCukru = iloscCukru;
                ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscMleka = iloscMleka;
                WylaczPanel();
            }
        }
    }
}
