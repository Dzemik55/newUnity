using UnityEngine;

public class NakladanieCiasta : MonoBehaviour
{
    public Vector3 spawnPosition;
    public static float initialSpawnHeight = 0.1f;
    public string literkaCiasta = "";
    RespawnCake cake;
    public PlateManager plates;


    private void OnMouseEnter()
    {
        if (OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony)
        {
            GetComponent<Outline>().enabled = true;
            Debug.Log("Myszka jest na mnie!");
        }
    }

    private void Start()
    {
        plates = GameObject.Find("PlateManagment").GetComponent<PlateManager>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony)
        {
            if (plates.currentPlateIndex >= plates.plateObjects.Length)
            {
                Debug.LogWarning("No plate object available for the current index.");
                return;
            }

            GameObject currentPlate = plates.plateObjects[plates.currentPlateIndex];
            if (currentPlate.transform.childCount > 0)
            {
                int objectCountOnPlate = currentPlate.transform.childCount;

                // Check if the plate already has an object
                if (objectCountOnPlate > 0)
                {
                    Debug.LogWarning("There is already an object on the plate.");
                    return;
                }
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

            plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters = plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters + literkaCiasta;
        }
    }


    private void OnMouseExit()
    {
        if (OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony)
        {
            GetComponent<Outline>().enabled = false;
        }
    }





    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}