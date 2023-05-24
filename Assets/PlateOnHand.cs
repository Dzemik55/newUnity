using UnityEngine;

public class PlateOnHand : MonoBehaviour
{
    public string playersPlate = "";

    private void Update()
    {

        if (PickUp.currentObject != null && PickUp.currentObject.tag == "Plate")
        {
            playersPlate = PickUp.currentObject.GetComponent<Plate>().plateLetters;
        }
    }
}
