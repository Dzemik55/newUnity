using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public GameObject[] plateObjects;
    public TransformData[] objectTransforms;
    public int currentPlateIndex = 0;

    private void Start()
    {
        plateObjects = GameObject.FindGameObjectsWithTag("Plate");
        plateObjects[currentPlateIndex].GetComponent<Outline>().enabled = true;

        objectTransforms = new TransformData[plateObjects.Length];

        // Iterate over the plateObjects array
        for (int i = 0; i < plateObjects.Length; i++)
        {
            GameObject obj = plateObjects[i];
            TransformData transformData = new TransformData(obj.transform.position, obj.transform.rotation);
            objectTransforms[i] = transformData;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchPlate();
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

    private void SwitchPlate()
    {
        plateObjects[currentPlateIndex].GetComponent<Outline>().enabled = false;
        currentPlateIndex++;
        Debug.Log(currentPlateIndex);
        if (currentPlateIndex >= plateObjects.Length)
        {
            currentPlateIndex = 0;
        }
        plateObjects[currentPlateIndex].GetComponent<Outline>().enabled = true;
    }
}
