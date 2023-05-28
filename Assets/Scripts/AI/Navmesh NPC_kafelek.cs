using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Navmesh_kafelek : MonoBehaviour
{
    GameObject newObject;
    int index;
    public string playerPlate = "";
    public int playerSugar = 0;
    public int playerMilk = 0;
    public int orderSugar = 0;
    public int orderMilk = 0;
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
    PlateManager platesManager;
    [SerializeField]
    int pickUpLayerMask;
    int layerMask;
    public Image zdjecieCiasta;
    string nazwaCiasta;
    public Image zdjecieKawy;
    public Image zdjecieMleka;
    public Image zdjecieCukru;
    public Image zdjecieTwarzy;
    public TextMeshPro cukier_text;
    public TextMeshPro mleko_text;
    string nazwaKawy;
    private void Start()
    {
        patienceDuration = 60f;
        platesManager = GameObject.FindObjectOfType<PlateManager>();
        orderMilk = UnityEngine.Random.Range(0, 6);
        orderSugar = UnityEngine.Random.Range(0, 6);
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
        randomIndex = random.Next(GameFlow.KafelekOrderValues.Count);

        // Access the key of the KeyValuePair at the random index
        randomKey = GameFlow.KafelekOrderValues[randomIndex].Key;
        randomValue = GameFlow.KafelekOrderValues[randomIndex].Value;
        Debug.Log(randomKey);
        // Print the randomly selected key to the console
        if (!isLeaving)
        {
            StartCoroutine(MoveToFreePoint());
        }

    }
    void Update()
    {
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

                punkty.score += (ocena * currentPatienceValue);

                punkty.efficency = (punkty.score / GameFlow.CustomerCount);

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
            if (randomValue == "Ciasto czekoladowe z Latte na miejscu")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "latte_m";
            }
            if (randomValue == "Ciasto czekoladowe z Americano na miejscu")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "americano_m";
            }
            if (randomValue == "Ciasto czekoladowe z Espresso na miejscu")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "espresso_m";
            }
            if (randomValue == "Ciasto czekoladowe z Cappucino na miejscu")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "cappuccino_m";
            }
            if (randomValue == "Ciasto czekoladowe z Latte na wynos")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "latte_w";
            }
            if (randomValue == "Ciasto czekoladowe z Americano na wynos")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "americano_w";
            }
            if (randomValue == "Ciasto czekoladowe z Espresso na wynos")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "espresso_w";
            }
            if (randomValue == "Ciasto czekoladowe z Cappucino na wynos")
            {
                nazwaCiasta = "czekoladowe";
                nazwaKawy = "cappuccino_w";
            }
            if (randomValue == "Ciasto kiwi z Latte na miejscu")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "latte_m";
            }
            if (randomValue == "Ciasto kiwi z Americano na miejscu")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "americano_m";
            }
            if (randomValue == "Ciasto kiwi z Espresso na miejscu")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "espresso_m";
            }
            if (randomValue == "Ciasto kiwi z Cappucino na miejscu")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "cappuccino_m";
            }
            if (randomValue == "Ciasto kiwi z Latte na wynos")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "latte_w";
            }
            if (randomValue == "Ciasto kiwi z Americano na wynos")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "americano_w";
            }
            if (randomValue == "Ciasto kiwi z Espresso na wynos")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "espresso_w";
            }
            if (randomValue == "Ciasto kiwi z Cappucino na wynos")
            {
                nazwaCiasta = "kiwi";
                nazwaKawy = "cappuccino_w";
            }
            if (randomValue == "Ciasto têczowe z Latte na miejscu")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "latte_m";
            }
            if (randomValue == "Ciasto têczowe z Americano na miejscu")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "americano_m";
            }
            if (randomValue == "Ciasto têczowe z Espresso na miejscu")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "espresso_m";
            }
            if (randomValue == "Ciasto têczowe z Cappucino na miejscu")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "cappuccino_m";
            }
            if (randomValue == "Ciasto têczowe z Latte na wynos")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "latte_w";
            }
            if (randomValue == "Ciasto têczowe z Americano na wynos")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "americano_w";
            }
            if (randomValue == "Ciasto têczowe z Espresso na wynos")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "espresso_w";
            }
            if (randomValue == "Ciasto têczowe z Cappucino na wynos")
            {
                nazwaCiasta = "têczowe";
                nazwaKawy = "cappuccino_w";
            }
            zdjecieCiasta.sprite = Resources.Load<Sprite>(nazwaCiasta);
            zdjecieCiasta.enabled = true;
            zdjecieKawy.sprite = Resources.Load<Sprite>(nazwaKawy);
            zdjecieKawy.enabled = true;
            zdjecieMleka.enabled = true;
            zdjecieCukru.enabled = true;
            mleko_text.gameObject.SetActive(true);
            cukier_text.gameObject.SetActive(true);
            mleko_text.text = orderMilk.ToString();
            cukier_text.text = orderSugar.ToString();





            if (!isLeaving)
            {
                lerpValue = currentPatienceValue / 100f; // wartoæ lerp miêdzy 0 a 1
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

                    if (playerPlate.Length > 0 && playerPlate[0] != 'C' && playerPlate[0] != 'R' && playerPlate[0] != 'Q')
                    {
                        // Zachowanie oryginalnej wartoci playersPlate
                        string originalPlate = playerPlate;

                        // Utworzenie nowego playersPlate z dodanym "Y" na pocz¹tku
                        playerPlate = "Y" + originalPlate;
                    }
                    playerMilk = GameObject.Find("First Person Controller").GetComponent<PlateOnHand>().playersMilk;
                    playerSugar = GameObject.Find("First Person Controller").GetComponent<PlateOnHand>().playersSugar;


                    if (String.IsNullOrEmpty(playerPlate))
                    {
                        Debug.Log("playerPlate: " + playerPlate);
                    }
                    else
                    {
                        Debug.Log("Gracz ma takie jedzonko ze sob¹: " + playerPlate);
                        if ((playerPlate.Contains("A") || playerPlate.Contains("E") || playerPlate.Contains("P") || playerPlate.Contains("L")) && (playerPlate.Contains("R") || playerPlate.Contains("Q") || playerPlate.Contains("C")))
                        {
                            ocena = SprawdzanieCukryMleka(playerSugar, playerMilk, orderSugar, orderMilk) + sprawdzanieSkladniki(playerPlate, randomKey);
                            Debug.Log(randomValue + " : " + ocena + " = " + SprawdzanieCukryMleka(playerSugar, playerMilk, orderSugar, orderMilk) + " + " + sprawdzanieSkladniki(playerPlate, randomKey) + " Ciasto i kawa");
                        }

                        else if ((playerPlate.Contains("A") || playerPlate.Contains("E") || playerPlate.Contains("P") || playerPlate.Contains("L")) && !playerPlate.Contains("R") && !playerPlate.Contains("Q") && !playerPlate.Contains("C"))
                        {
                            ocena = SprawdzanieCukryMleka(playerSugar, playerMilk, orderSugar, orderMilk) + sprawdzanieSkladniki(playerPlate, randomKey);
                            Debug.Log(randomValue + " : " + ocena + " = " + SprawdzanieCukryMleka(playerSugar, playerMilk, orderSugar, orderMilk) + " + " + sprawdzanieSkladniki(playerPlate, randomKey) + " Sama kawa");
                        }
                        else if (!playerPlate.Contains("A") || !playerPlate.Contains("E") || !playerPlate.Contains("P") || !playerPlate.Contains("L"))
                        {
                            ocena = sprawdzanieSkladniki(playerPlate, randomKey);
                            Debug.Log(randomValue + " : " + ocena + " = " + sprawdzanieSkladniki(playerPlate, randomKey) + " Samo ciasto");
                        }
                        isLeaving = true;
                        GameFlow.CustomerCount++;
                        if (PickUp.currentObject != null)
                            PickUp.currentObject.GetComponent<PickUp>().isPickedUp = false;

                        if (PickUp.currentObject != null)
                        {
                            index = Array.IndexOf(platesManager.plateObjects, PickUp.currentObject);

                            Destroy(PickUp.currentObject);
                        }
                        newObject = Instantiate(Plate, platesManager.objectTransforms[index].position, platesManager.objectTransforms[index].rotation);
                        platesManager.plateObjects[index] = newObject;
                        platesManager.plateObjects[platesManager.currentPlateIndex].GetComponent<Outline>().enabled = true;

                        zdjecieCiasta.enabled = false;
                        zdjecieKawy.enabled = false;
                        zdjecieCukru.enabled = false;
                        zdjecieMleka.enabled = false;
                        cukier_text.gameObject.SetActive(false);
                        mleko_text.gameObject.SetActive(false);
                        if (ocena <= 0.3)
                        {
                            zdjecieTwarzy.sprite = Resources.Load<Sprite>("sadFace");
                        }
                        punkty.score += (ocena * currentPatienceValue);
                        punkty.efficency = (punkty.score / GameFlow.CustomerCount);
                        if (ocena * currentPatienceValue < 40f)
                        {
                            zdjecieTwarzy.sprite = Resources.Load<Sprite>("sadFace");
                        }
                        else if (ocena * currentPatienceValue < 70f)
                        {
                            zdjecieTwarzy.sprite = Resources.Load<Sprite>("neutralFace");
                        }
                        else
                        {
                            zdjecieTwarzy.sprite = Resources.Load<Sprite>("happyFace");
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
                wynik += 0.75f / x;
            }
            else
            {
                wszystkiePoprawne = false;
            }
        }

        if (wszystkiePoprawne)
        {
            wynik = 0.75f;
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

    public float SprawdzanieCukryMleka(int playersSugar, int playersMilk, int randomSugar, int randomMilk)
    {
        float points = 0.25f; // Pocz¹tkowa wartoæ punktów

        // Obliczanie ró¿nicy cukru i mleka miêdzy graczem a klientem
        int sugarDifference = Mathf.Abs(playerSugar - randomSugar);
        int milkDifference = Mathf.Abs(playerMilk - randomMilk);

        // Obliczanie procentowej obni¿ki punktów na podstawie ró¿nicy cukru i mleka
        float sugarPenalty = sugarDifference * 0.1f;
        float milkPenalty = milkDifference * 0.1f;

        // Obni¿anie punktów o procentow¹ wartoæ ró¿nicy
        points -= points * (sugarPenalty + milkPenalty);

        return points;
    }



}