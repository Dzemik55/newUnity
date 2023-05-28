using UnityEngine;

public class OdslonBabeczki : MonoBehaviour
{
    bool BabeczkiZakryte = true;

    private void OnMouseDown()
    {
        if (BabeczkiZakryte)
        {
            transform.localPosition = new Vector3(0.0120700002f, -0.0120599996f, -0.0150800003f);
            BabeczkiZakryte = false;
        }
        else
        {
            transform.localPosition = new Vector3(8.82148754e-08f, 0.00314546051f, -3.33786012e-08f);
            BabeczkiZakryte = true;
        }
        GetComponent<Outline>().enabled = false;
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
