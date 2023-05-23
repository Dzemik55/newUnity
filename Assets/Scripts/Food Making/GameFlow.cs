using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    private int spawnCount = 0;
    private float spawningRange = 15f;
    public static int CustomerCount = 0;
    public GameObject Customers;
    public GameObject spawn;
    private Vector3 spawnPoint;
    private bool isSpawning = false;
    public static int plateValue = 00000;
    public static List<KeyValuePair<string, string>> orderValues = new List<KeyValuePair<string, string>>
{
    new KeyValuePair<string, string>("bcKhXlKasB", "Super Meaty"),
    new KeyValuePair<string, string>("bcKxCbcKXpB", "Double Burger"),
    new KeyValuePair<string, string>("bclPtCxoB", "Very Vegetable"),
    new KeyValuePair<string, string>("brPOliqeB", "What?")
};


    void Start()
    {
        //
        Customers = GameObject.Find("Customers");
        // Create a new instance of the Random class
        spawnPoint = GameObject.Find("Spawn").transform.position;

    }


    void Update()
    {
        if (!isSpawning && Customers.transform.childCount < 4)
        {
            isSpawning = true;
            StartCoroutine(Spawning());
        }
    }

    IEnumerator Spawning()
    {

        if (punkty.efficency > 90)
        {
            spawningRange = Random.Range(5.0f, 15.0f);
        }
        else if (punkty.efficency > 70)
        {
            spawningRange = Random.Range(10.0f, 20.0f);
        }
        else if (punkty.efficency > 50)
        {
            spawningRange = Random.Range(15.0f, 25.0f);
        }
        else if (punkty.efficency > 30)
        {
            spawningRange = Random.Range(20.0f, 30.0f);
        }

        else if (punkty.efficency == 0 && spawnCount < 4)
        {
            spawningRange = 1f;
        }
        else if (punkty.efficency < 30)
        {
            spawningRange = Random.Range(20.0f, 30.0f);
        }
        yield return new WaitForSeconds(spawningRange);
        GameObject spawned = Instantiate(spawn, spawnPoint, Quaternion.identity);
        spawned.name = spawned.name + Customers.transform.childCount + 1;
        spawned.transform.SetParent(Customers.transform);
        Debug.Log("Spawning Range: " + spawningRange + ", Efficency: " + punkty.efficency);
        spawnCount++;
        isSpawning = false;
    }

}
