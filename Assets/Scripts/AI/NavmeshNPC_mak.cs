using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Navmesh_mak : MonoBehaviour
{
    int index;
    public string playerPlate = "";
    public GameObject Tekst;
    public GameObject Plate;
    public Gradient Gradient = new Gradient();
    public Material Material;
    public GameObject wynik;
    private float lerpValue;
    public GameObject WaypointsList;
    public GameObject RegistersList;
    public GameObject ExitList;
    public bool isActive = true;
    public List<GameObject> exitPoints;
    public List<GameObject> registers;
    public List<GameObject> waypoints;
    private NavMeshAgent navMeshAgent;
    public GameObject currentRegister;
    private int lastCheckedIndex = -1;
    public bool isLeaving = false;
    public float patienceDuration = 5f; // Czas w sekundach
    public float maxPatienceValue = 100f;
    public float minPatienceValue = 0f;
    public float timeElapsed = 0f;
    public float ocena = 1;
    public float currentPatienceValue;
    public int randomIndex;
    public string randomValue;
    public string randomKey;
    Click click;
    Click[] clickObjects;
    [SerializeField]
    int pickUpLayerMask;
    int layerMask;
    public Image zdjecieBurgera;
    string nazwaBurgera;
    private void Start()
    {
        click = GameObject.Find("Cheese_Slice").GetComponent<Click>();
        patienceDuration = 60f;
        clickObjects = FindObjectsOfType<Click>();
        pickUpLayerMask = LayerMask.GetMask("Food");
        layerMask = ~pickUpLayerMask;
        Material = GetComponentInChildren<MeshRenderer>().material;
        Material.color = Color.green;
        wynik = GameObject.Find("wynik");
        currentPatienceValue = maxPatienceValue;
        WaypointsList = GameObject.Find("Queue");
        RegistersList = GameObject.Find("Register");
        ExitList = GameObject.Find("Exit");
        waypoints.Clear();
        exitPoints.Clear();
        foreach (Transform child in WaypointsList.transform)
        {
            if (child == null) { continue; }
            waypoints.Add(child.gameObject);
        }
        foreach (Transform child in RegistersList.transform)
        {
            if (child == null) { continue; }
            registers.Add(child.gameObject);
        }
        foreach (Transform child in ExitList.transform)
        {
            if (child == null) { continue; }
            exitPoints.Add(child.gameObject);
        }
        waypoints.Reverse();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //wybieranie jedzenia
        System.Random random = new System.Random();

        // Generate a random index between 0 and the length of the list
        randomIndex = random.Next(GameFlow.orderValues.Count);

        // Access the key of the KeyValuePair at the random index
        randomKey = GameFlow.orderValues[randomIndex].Key;
        randomValue = GameFlow.orderValues[randomIndex].Value;
        Debug.Log(randomKey);
        // Print the randomly selected key to the console
        if (!isLeaving)
        {
            StartCoroutine(MoveToFreePoint());
        }

    }

    private IEnumerator MoveToFreePoint()
    {
        while (true)
        {
            bool foundFreePoint = false;
            if (!isActive)
            {
                break;
            }

            // Sprawdzenie, czy istnieje wolna kasa
            for (int i = registers.Count - 1; i >= 0; i--)
            {
                isTaken isTakenComponent = registers[i].GetComponent<isTaken>();
                if (isTakenComponent != null && !isTakenComponent.Occupied && !isLeaving && isActive)
                {
                    isTakenComponent.Occupied = true;
                    currentRegister = registers[i];
                    navMeshAgent.SetDestination(registers[i].transform.position);
                    yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);
                    yield return StartCoroutine(WaitForInteraction()); // Wait for player interaction
                    foundFreePoint = true;
                    break; // Move to the first free register only
                }
            }

            // Sprawdzenie, czy w kolejce jest wolne miejsce
            if (!foundFreePoint && !isLeaving && isActive)
            {
                bool foundFreeQueuePoint = false;
                for (int i = 0; i < waypoints.Count; i++)
                {
                    isTaken isTakenComponent = waypoints[i].GetComponent<isTaken>();
                    if (isTakenComponent != null && !isTakenComponent.Occupied)
                    {
                        isTakenComponent.Occupied = true;
                        navMeshAgent.SetDestination(waypoints[i].transform.position);
                        yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);
                        yield return new WaitForSeconds(1f); // Wait for 1 second at the destination
                        isTakenComponent.Occupied = false;
                        foundFreeQueuePoint = true;
                        break; // Move to the first free queue point only
                    }
                }
                // If there are no free queue points, wait for some time before checking again
                if (!foundFreeQueuePoint)
                {
                    yield return new WaitForSeconds(1f);
                }
            }

            if (isLeaving)
            {
                currentRegister.GetComponent<isTaken>().Occupied = false; // Free the register
                                                                          // Move to each exit point and leave the building
                foreach (GameObject exitPoint in exitPoints)
                {
                    navMeshAgent.SetDestination(exitPoint.transform.position);
                    yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

                    // Wait for some time before moving to the next exit point
                    yield return new WaitForSeconds(1f);
                }

                // Reset the current register and disable the agent
                isActive = false;
                isLeaving = false;

                Debug.Log("Customers count: " + GameFlow.CustomerCount + ", Punkty: " + punkty.score + ", Efficency: " + punkty.efficency);
                Destroy(gameObject);
            }
        }
    }


    private IEnumerator WaitForInteraction()
    {
        // Wait for player to look at the agent and press F
        while (true)
        {


            // Tekst.GetComponent<TextMeshPro>().text = randomKey + "+ " + randomValue;
            if (randomValue == "Super Meaty")
            {
                nazwaBurgera = "super_meaty";
            }
            if (randomValue == "Very Vegetable")
            {
                nazwaBurgera = "vegetable";
            }
            if (randomValue == "What?")
            {
                nazwaBurgera = "what";
            }
            if (randomValue == "Double Burger")
            {
                nazwaBurgera = "double";
            }
            zdjecieBurgera.sprite = Resources.Load<Sprite>(nazwaBurgera);
            zdjecieBurgera.enabled = true;


            if (!isLeaving)
            {
                lerpValue = currentPatienceValue / 100f; // warto�� lerp mi�dzy 0 a 1
                Material.color = Color.Lerp(Color.red, Color.green, currentPatienceValue / 100f);
                timeElapsed += Time.deltaTime;
                if (timeElapsed > patienceDuration)
                {
                    currentPatienceValue = Mathf.Lerp(maxPatienceValue, minPatienceValue, Mathf.InverseLerp(0f, patienceDuration, timeElapsed - patienceDuration));
                }
                else
                {
                    currentPatienceValue = 100;
                }
            }
            if (currentPatienceValue == 0)
            {
                isLeaving = true;
                GameFlow.CustomerCount++;
                break;
            }
            // Check if the player is looking at the agent
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit, float.PositiveInfinity, layerMask) && hit.collider.gameObject == gameObject)
            {
                // Player is looking at the agent, wait for F key press

                if (Input.GetKeyDown(KeyCode.F))
                {
                    playerPlate = GameObject.Find("First Person Controller").GetComponent<PlateOnHand>().playersPlate;
                    if (String.IsNullOrEmpty(playerPlate))
                    {
                        Debug.Log("playerPlate: " + playerPlate);
                    }
                    else
                    {
                        Debug.Log("Gracz ma takie jedzonko ze sob�: " + playerPlate);
                        ocena = sprawdzanieKolejnosc(playerPlate, randomKey) + sprawdzanieSkladniki(playerPlate, randomKey);
                        bool containsLetters = playerPlate.Contains("A") || playerPlate.Contains("k") || playerPlate.Contains("S");
                        if (containsLetters)
                        {
                            float zabranePunkty = ocena * 0.1f;
                            float nowePunkty = ocena * 0.9f;
                            ocena = nowePunkty;
                            Debug.Log("Zabrano tyle punkt�w: " + zabranePunkty);
                        }
                        Debug.Log(randomValue + " : " + ocena + " = " + sprawdzanieKolejnosc(playerPlate, randomKey) + " + " + sprawdzanieSkladniki(playerPlate, randomKey));
                        isLeaving = true;
                        GameFlow.CustomerCount++;
                        if (PickUp.currentObject != null)
                            PickUp.currentObject.GetComponent<PickUp>().isPickedUp = false;

                        if (PickUp.currentObject != null)
                        {
                            index = Array.IndexOf(click.plateObjects, PickUp.currentObject);

                            Destroy(PickUp.currentObject);
                        }
                        GameObject newObject = Instantiate(Plate, click.objectTransforms[index].position, click.objectTransforms[index].rotation);
                        foreach (Click clickObject in clickObjects)
                        {
                            clickObject.plateObjects[index] = newObject;
                            clickObject.plateObjects[clickObject.currentPlateIndex].GetComponent<Outline>().enabled = true;
                        }

                        punkty.score += (ocena * currentPatienceValue);
                        punkty.efficency = (punkty.score / GameFlow.CustomerCount);
                        if (ocena * currentPatienceValue < 40f)
                        {
                            zdjecieBurgera.sprite = Resources.Load<Sprite>("sadFace");
                        }
                        else if (ocena * currentPatienceValue < 70f)
                        {
                            zdjecieBurgera.sprite = Resources.Load<Sprite>("neutralFace");
                        }
                        else
                        {
                            zdjecieBurgera.sprite = Resources.Load<Sprite>("happyFace");
                        }
                        break;
                    }
                }
            }
            else
            {
                // Player is not looking at the agent, don't wait for interaction
                isLeaving = false;
            }
            yield return null;
        }
    }
    public float sprawdzanieSkladniki(string playerPlate, string randomKey)
    {
        bool wszystkiePoprawne = true;
        float wynik = 0f;

        int x = Math.Max(playerPlate.Length, randomKey.Length);

        for (int i = 0; i < x; i++)
        {
            if (i < playerPlate.Length && i < randomKey.Length && playerPlate[i] == randomKey[i])
            {
                wynik += 0.7f / x;
            }
            else
            {
                wszystkiePoprawne = false;
            }
        }

        if (wszystkiePoprawne)
        {
            wynik = 0.7f;
        }

        return wynik;
    }
    public float sprawdzanieKolejnosc(string playerPlate, string randomKey)
    {
        int i = 0;
        int j = 0;
        float wynik = 0f;
        int len1 = playerPlate.Length;
        int len2 = randomKey.Length;

        while (i < len1 && j < len2)
        {
            if (playerPlate[i] == randomKey[j])
            {
                i++;
                j++;
            }
            else
            {
                j++;
            }
        }

        if (i == len1)
        {
            wynik = 0.3f;
        }
        else
        {
            float dlugoscWieksza = len1 > len2 ? len1 : len2;
            wynik = 0.3f * (i / dlugoscWieksza);
        }

        return wynik;
    }

}