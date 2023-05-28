using UnityEngine;

public class ResetNoza : MonoBehaviour
{
    Vector3 pozycjaStartowa;
    Quaternion rotacjaStartowa;
    // Start is called before the first frame update
    void Start()
    {
        pozycjaStartowa = transform.position;
        rotacjaStartowa = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !OdlozNoz.nozPodniesiony)
        {
            transform.position = pozycjaStartowa;
            transform.rotation = rotacjaStartowa;
            OdlozNoz.nozOdlozony = true;
            OdlozNoz.nozPodniesiony = false;
            transform.SetParent(null);
        }
    }

    private void OnMouseOver()
    {
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }
}
