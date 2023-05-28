using UnityEngine;

public class Talerzyk : MonoBehaviour
{
    private void OnMouseEnter()
    {
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }

}
