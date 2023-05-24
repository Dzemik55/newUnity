using UnityEngine;

public class Plate : MonoBehaviour
{
    public float spawnHeight = 0f;
    public string plateLetters = "";
    // Time interval between logs
    public string DanieGracza = "";
    public int childCount = 0;
    GameObject gracz;

    private void Update()
    {
        if (gameObject.GetComponent<PickUp>().isPickedUp)
        {
            DanieGracza = plateLetters;
        }
        else
        {
            DanieGracza = "";
        }


    }

    private void Start()
    {
        gracz = GameObject.Find("First Person Controller");
        gracz.GetComponent<PlateOnHand>().playersPlate = "";
    }






}