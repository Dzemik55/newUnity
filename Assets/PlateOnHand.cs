using UnityEngine;

public class PlateOnHand : MonoBehaviour
{
    public string playersPlate = "";
    public int playersSugar = 0;
    public int playersMilk = 0;

    private void Update()
    {

        if (PickUp.currentObject != null && PickUp.currentObject.tag == "Plate")
        {
            playersPlate = PickUp.currentObject.GetComponent<Plate>().plateLetters;
            playersSugar = PickUp.currentObject.GetComponent<Plate>().plateSugar;
            playersMilk = PickUp.currentObject.GetComponent<Plate>().plateMilk;
        }
    }
}
