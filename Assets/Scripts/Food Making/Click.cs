using UnityEngine;

public class Click : MonoBehaviour
{
    public Transform cloneObj;
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
    public GameObject particlePrefab;


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
                particlePrefab = Resources.Load<GameObject>("Smoke");
                GameObject Smoke = Instantiate(particlePrefab);
                Smoke.transform.SetParent(spawnedObject.transform, false);
                ParticleSystem.MainModule Smokeps = Smoke.GetComponent<ParticleSystem>().main;
                Color startColor = Smokeps.startColor.color;
                startColor = Color.white;
                startColor.a = 0.5f;
                Smokeps.startColor = startColor;
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
            spawnedObject.GetComponent<PlacedIngredient>().enabled = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchPlate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchPatelnia();
        }




    }

    private void Start()
    {
        plateObjects = GameObject.FindGameObjectsWithTag("Plate");
        patelniaObjects = GameObject.FindGameObjectsWithTag("Patelnia");
        gameObject.GetComponent<Outline>().enabled = false;
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
