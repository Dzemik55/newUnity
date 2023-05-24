using UnityEngine;

public class NakladanieCiasta : MonoBehaviour
{
    public static GameObject[] plateObjects;
    public int currentPlateIndex = 0;
    public Vector3 spawnPosition;
    public static float initialSpawnHeight = 0.1f;
    public string literkaCiasta = "";

    private void Start()
    {
        plateObjects = GameObject.FindGameObjectsWithTag("Plate");
        plateObjects[currentPlateIndex].GetComponent<Outline>().enabled = true;
        Debug.Log(plateObjects.Length);
    }
    private void OnMouseEnter()
    {
        if (OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony)
        {
            GetComponent<Outline>().enabled = true;
            Debug.Log("Myszka jest na mnie!");
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony)
        {
            if (currentPlateIndex >= plateObjects.Length)
            {
                Debug.LogWarning("No plate object available for the current index.");
                return;
            }

            GameObject currentPlate = plateObjects[currentPlateIndex];
            int objectCountOnPlate = currentPlate.transform.childCount;

            // Check if the plate already has an object
            if (objectCountOnPlate > 0)
            {
                Debug.LogWarning("There is already an object on the plate.");
                return;
            }

            spawnPosition = currentPlate.transform.position;

            // Calculate the height of the object
            float objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;

            // Calculate the center position of the plate
            Vector3 plateCenter = currentPlate.GetComponent<Renderer>().bounds.center;

            // Set the spawn position slightly above the center of the plate
            spawnPosition = plateCenter + Vector3.up * objectHeight * 0.8f;

            transform.SetParent(null);
            transform.SetParent(currentPlate.transform);
            transform.position = spawnPosition;

            plateObjects[currentPlateIndex].GetComponent<Plate>().plateLetters = plateObjects[currentPlateIndex].GetComponent<Plate>().plateLetters + literkaCiasta;
        }
    }


    private void OnMouseExit()
    {
        if (OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony)
        {
            GetComponent<Outline>().enabled = false;
        }
    }

    private void SwitchPlate()
    {
        plateObjects[currentPlateIndex].GetComponent<Outline>().enabled = false;
        currentPlateIndex++;
        if (currentPlateIndex >= plateObjects.Length)
        {
            currentPlateIndex = 0;
        }
        plateObjects[currentPlateIndex].GetComponent<Outline>().enabled = true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchPlate();
        }
    }


}
