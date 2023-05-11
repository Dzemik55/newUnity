using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public GameObject Customers;
    public GameObject spawn;
    private bool isSpawning = false;
    public static int plateValue = 00000;
    public static List<KeyValuePair<string, string>> orderValues = new List<KeyValuePair<string, string>>
{
    new KeyValuePair<string, string>("qwertyuiop", "Super Meaty"),
    new KeyValuePair<string, string>("qewrtyuiop", "Double Burger"),
    new KeyValuePair<string, string>("qwertttyuiop", "Very Vegetable"),
    new KeyValuePair<string, string>("qwop", "What?")
};


    void Start()
    {
        //
        Customers = GameObject.Find("Customers");
        // Create a new instance of the Random class
        

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
        yield return new WaitForSeconds(2.0f);
        GameObject spawned = Instantiate(spawn, new Vector3(-3.759f, 0, -23.27f), Quaternion.identity);
        spawned.name = spawned.name + Customers.transform.childCount + 1;
        spawned.transform.SetParent(Customers.transform);
        isSpawning = false;
    }
}
