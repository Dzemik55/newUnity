using System;
using UnityEngine;

public class PlacedIngredient : MonoBehaviour
{

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse button click
        {
            if (Array.IndexOf(Click.instance.plateObjects, gameObject.transform.parent.gameObject) != -1)
            {
                DeleteIngredients(transform.parent.gameObject);
            }
        }
    }

    private void DeleteIngredients(GameObject plateObject)
    {
        // Check if the plate object exists and has ingredients
        Plate plate = plateObject.GetComponent<Plate>();
        if (plate != null && plate.plateLetters.Length > 0)
        {
            // Destroy all the ingredient objects on the plate
            foreach (Transform child in plateObject.transform)
            {
                Destroy(child.gameObject);
            }

            // Reset the plate's spawn height and plate letters
            plate.spawnHeight = 0f;
            plate.plateLetters = string.Empty;
        }
    }
}
