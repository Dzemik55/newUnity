using UnityEngine;

public class PlateOnHand : MonoBehaviour
{
    public string playersPlate = "";

    private void Update()
    {

        if (PickUp.currentObject != null)
        {
            playersPlate = PickUp.currentObject.GetComponent<Plate>().plateLetters;
        }
    }
}
