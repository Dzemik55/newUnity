using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeACoffe : MonoBehaviour
{
    public GameObject panel;
    bool isPanelActive = false;
    public Slider cukierSlider;
    public Slider mlekoSlider;
    public GameObject KawkaLatteObject;
    public GameObject KawkaCappucinoObject;
    public GameObject KawkaAmericanoObject;
    public GameObject KawkaEspressoObject;
    public GameObject NaWynosObject;
    private GameObject ostatnioUtworzonyObiekt;
    public TextMeshProUGUI IstniejeKawkaText;
    public Toggle naWynosToggle;
    public bool naWynos = false;

    public PauseMenu pauseMenu;
    public float interactionDistance;
    public Transform playerTransform;

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
                pauseMenu.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                firstPersonLook.canRotate = true;
                GameObject.Find("First Person Controller").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                panel.GetComponent<Image>().enabled = false;
                isPanelActive = false;
                iloscCukru = 0;
                iloscMleka = 0;
                cukierSlider.value = 0;
                mlekoSlider.value = 0;
                naWynosToggle.isOn = false;
                naWynos = false;
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
                pauseMenu.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                firstPersonLook.canRotate = false;
                GameObject.Find("First Person Controller").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                panel.GetComponent<Image>().enabled = true;
                isPanelActive = true;
                Transform[] children = panel.GetComponentsInChildren<Transform>(true);

                foreach (Transform child in children)
                {
                    if (child.gameObject.name == "ZabierzKawkeText")
                    {
                        Debug.Log(child.gameObject.name);
                        child.gameObject.SetActive(false);
                    }
                    else
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!enabled) return;
        interactionDistance = (Vector3.Distance(transform.position, playerTransform.position));
        if (interactionDistance <= 3f)
        {
            WlaczPanel();
        }
    }

    public void DodajCukier()
    {
        if (iloscCukru < 5)
        {
            cukierSlider.value += 0.2f;
            iloscCukru += 1;
        }
        Debug.Log(iloscCukru);
    }

    public void OdejmikCukier()
    {
        if (iloscCukru > 0)
        {
            cukierSlider.value -= 0.2f;
            iloscCukru -= 1;
        }
        Debug.Log(iloscCukru);
    }

    public void DodajMleko()
    {
        if (iloscMleka < 5)
        {
            mlekoSlider.value += 0.2f;
            iloscMleka += 1;
        }
        Debug.Log(iloscMleka);
    }

    public void OdejmikMleko()
    {
        if (iloscMleka > 0)
        {
            mlekoSlider.value -= 0.2f;
            iloscMleka -= 1;
        }
        Debug.Log(iloscMleka);
    }

    public void NaWynosAlboNie()
    {
        if (naWynosToggle.isOn)
        {
            naWynos = true;
            Debug.Log(naWynos);
        }

        if (!naWynosToggle.isOn)
        {
            naWynos = false;
            Debug.Log(naWynos);
        }

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
        if (buttonName == "Cappucino_button")
        {
            Kawa = "Cappucino";
        }
        if (buttonName == "Espresso_button")
        {
            Kawa = "Espresso";
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
                StopAllCoroutines();
                StartCoroutine(WyswietlKomuniktat(IstniejeKawkaText));
                break;
            }

            else
            {
                // There is no Kawka object at the given position
                if (Kawa == "Latte" && !naWynos)
                {
                    ostatnioUtworzonyObiekt = Instantiate(KawkaLatteObject, new Vector3(2.95499992f, 1.49699998f, -4.29699993f), Quaternion.identity);
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().Kawa = Kawa;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscCukru = iloscCukru;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscMleka = iloscMleka;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().naWynos = naWynos;
                    WylaczPanel();
                }
                if (Kawa == "Americano" && !naWynos)
                {
                    ostatnioUtworzonyObiekt = Instantiate(KawkaAmericanoObject, positionToCheck, Quaternion.Euler(-90f,0,0));
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().Kawa = Kawa;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscCukru = iloscCukru;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscMleka = iloscMleka;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().naWynos = naWynos;
                    WylaczPanel();
                }
                if (Kawa == "Cappucino" && !naWynos)
                {
                    ostatnioUtworzonyObiekt = Instantiate(KawkaCappucinoObject, positionToCheck, Quaternion.Euler(-90f, 0, 0));
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().Kawa = Kawa;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscCukru = iloscCukru;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscMleka = iloscMleka;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().naWynos = naWynos;
                    WylaczPanel();
                }
                if (Kawa == "Espresso" && !naWynos)
                {
                    ostatnioUtworzonyObiekt = Instantiate(KawkaEspressoObject, positionToCheck, Quaternion.identity);
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().Kawa = Kawa;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscCukru = iloscCukru;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscMleka = iloscMleka;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().naWynos = naWynos;
                    WylaczPanel();
                }

                if (naWynos)
                {
                    Quaternion rotation = Quaternion.Euler(-90, 0, 0);
                    positionToCheck = new Vector3(2.95499992f, 1.58800006f, -4.29699993f);
                    ostatnioUtworzonyObiekt = Instantiate(NaWynosObject, positionToCheck, rotation);
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().Kawa = Kawa;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscCukru = iloscCukru;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().iloscMleka = iloscMleka;
                    ostatnioUtworzonyObiekt.GetComponent<Kawka>().naWynos = naWynos;
                    WylaczPanel();
                }
            }
        }
    }

    IEnumerator WyswietlKomuniktat(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        text.gameObject.SetActive(false);

    }
}
