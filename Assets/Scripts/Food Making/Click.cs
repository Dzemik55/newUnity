using UnityEngine;

public class Click : MonoBehaviour
{
    public Transform cloneObj;
    private GameObject patelnia;
    public int foodValue;
    public GameObject[] plateObjects;
    public static GameObject[] patelniaObjects;
    public TransformData[] objectTransforms;
    public int currentPlateIndex = 0;
    public int currentPatelniaIndex = 0;
    public static float spawnHeight = 0f;
    public static float initialSpawnHeight = 0.1f; // Adjust this value as needed
    public float spawnHeightOffset = 0; // Adjust this value as needed
    public static Click instance;
    public Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
    GameObject spawnedObject;
    public string ingredientLetter;

    private void Awake()
    {
        instance = this;
    }

    private void OnMouseDown()
    {
        if (currentPlateIndex >= plateObjects.Length)
        {
            Debug.LogWarning("No plate object available for the current index.");
            return;
        }

        spawnPosition = plateObjects[currentPlateIndex].transform.position;
        Vector3 patelniaSpawnPosition = patelniaObjects[currentPatelniaIndex].transform.position;

        float objectHeight = cloneObj.GetComponent<Renderer>().bounds.size.y;

        if (plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight == 0f && (gameObject.name != "Roast_Slice" || gameObject.name != "Bacon_Slice" || gameObject.name != "Sausage_Slice")) // For the first spawned object
        {
            float plateHeight = plateObjects[currentPlateIndex].GetComponent<Renderer>().bounds.size.y;
            plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight = plateHeight + initialSpawnHeight;
            //plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight = spawnHeight;
        }
        else if (gameObject.name != "Roast_Slice" && gameObject.name != "Bacon_Slice" && gameObject.name != "Sausage_Slice")
        {
            plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight += objectHeight + spawnHeightOffset;
            //plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight = spawnHeight;
        }

        spawnPosition += Vector3.up * plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight;
        if (gameObject.name == "Roast_Slice" || gameObject.name == "Bacon_Slice" || gameObject.name == "Sausage_Slice")
        {
            if (patelniaObjects[currentPatelniaIndex].GetComponent<IloscKotletow>().iloscKotletow == 0)
            {
                spawnedObject = Instantiate(cloneObj, patelniaSpawnPosition, cloneObj.rotation).gameObject;
                spawnedObject.GetComponent<CookMove>().PatelniaNaKtorejLeze = patelniaObjects[currentPatelniaIndex];
                spawnedObject.GetComponent<CookMove>().PatelniaNaKtorejLeze.GetComponent<IloscKotletow>().iloscKotletow = 1;
            }
            else
            {
                Debug.Log("Nie mo¿esz po³o¿yæ kotletów na tej patelni!");
            }

        }

        else
        {
            spawnedObject = Instantiate(cloneObj, spawnPosition, cloneObj.rotation).gameObject;
            spawnedObject.transform.parent = plateObjects[currentPlateIndex].transform;
            plateObjects[currentPlateIndex].GetComponent<Plate>().plateLetters = plateObjects[currentPlateIndex].GetComponent<Plate>().plateLetters + ingredientLetter;
            Debug.Log("Spawn position = " + spawnPosition + ", spawnHeight = " + plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchPatelnia();
        }
    }

    private void Start()
    {
        patelniaObjects = GameObject.FindGameObjectsWithTag("Patelnia");
        Debug.Log("Iloœæ patelni: " + patelniaObjects.Length);
        gameObject.GetComponent<Outline>().enabled = false;
        patelnia = GameObject.Find("pan_3_2");
        plateObjects[currentPlateIndex].GetComponent<Outline>().enabled = true;
        patelniaObjects[currentPatelniaIndex].GetComponent<Outline>().enabled = true;
        Invoke("DelayedStart", 2f);


    }

    private void DelayedStart()
    {
        objectTransforms = new TransformData[plateObjects.Length];

        // Iterate over the plateObjects array
        for (int i = 0; i < plateObjects.Length; i++)
        {
            GameObject obj = plateObjects[i];
            TransformData transformData = new TransformData(obj.transform.position, obj.transform.rotation);
            objectTransforms[i] = transformData;
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
        // spawnHeight = plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight;
        // Debug.Log("Spawn height of this plate " + currentPlateIndex + ": " + spawnHeight);

        // Optionally, you can provide visual feedback to the player indicating the active plate object.
        // For example, you can highlight the active plate object or display a UI indicator.
    }

    private void SwitchPatelnia()
    {
        patelniaObjects[currentPatelniaIndex].GetComponent<Outline>().enabled = false;
        currentPatelniaIndex++;
        if (currentPatelniaIndex >= patelniaObjects.Length)
        {
            currentPatelniaIndex = 0;
        }
        patelniaObjects[currentPatelniaIndex].GetComponent<Outline>().enabled = true;
        // spawnHeight = plateObjects[currentPlateIndex].GetComponent<Plate>().spawnHeight;
        // Debug.Log("Spawn height of this plate " + currentPlateIndex + ": " + spawnHeight);

        // Optionally, you can provide visual feedback to the player indicating the active plate object.
        // For example, you can highlight the active plate object or display a UI indicator.
    }

    private void OnMouseOver()
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().enabled = false;
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