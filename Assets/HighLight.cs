using UnityEngine;

public class HighLight : MonoBehaviour
{
    private Material originalMaterial;
    public Material highlightMaterial;


    private void OnMouseEnter()
    {
        // Store the original material
        originalMaterial = GetComponent<Renderer>().material;
        // Assign the highlight material
        GetComponent<Renderer>().material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        // Revert to the original material
        GetComponent<Renderer>().material = originalMaterial;
    }
}
