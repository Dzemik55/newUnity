using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static List<KeyValuePair<string, string>> KafelekOrderValues = new List<KeyValuePair<string, string>>
{
   new KeyValuePair<string, string>("CLm", "Ciasto czekoladowe z Latte na miejscu"),
new KeyValuePair<string, string>("CAm", "Ciasto czekoladowe z Americano na miejscu"),
new KeyValuePair<string, string>("CEm", "Ciasto czekoladowe z Espresso na miejscu"),
new KeyValuePair<string, string>("CPm", "Ciasto czekoladowe z Cappucino na miejscu"),
new KeyValuePair<string, string>("CLw", "Ciasto czekoladowe z Latte na wynos"),
new KeyValuePair<string, string>("CAw", "Ciasto czekoladowe z Americano na wynos"),
new KeyValuePair<string, string>("CEw", "Ciasto czekoladowe z Espresso na wynos"),
new KeyValuePair<string, string>("CPw", "Ciasto czekoladowe z Cappucino na wynos"),
new KeyValuePair<string, string>("QLm", "Ciasto kiwi z Latte na miejscu"),
new KeyValuePair<string, string>("QAm", "Ciasto kiwi z Americano na miejscu"),
new KeyValuePair<string, string>("QEm", "Ciasto kiwi z Espresso na miejscu"),
new KeyValuePair<string, string>("QPm", "Ciasto kiwi z Cappucino na miejscu"),
new KeyValuePair<string, string>("QLw", "Ciasto kiwi z Latte na wynos"),
new KeyValuePair<string, string>("QAw", "Ciasto kiwi z Americano na wynos"),
new KeyValuePair<string, string>("QEw", "Ciasto kiwi z Espresso na wynos"),
new KeyValuePair<string, string>("QPw", "Ciasto kiwi z Cappucino na wynos"),
new KeyValuePair<string, string>("RLm", "Ciasto têczowe z Latte na miejscu"),
new KeyValuePair<string, string>("RAm", "Ciasto têczowe z Americano na miejscu"),
new KeyValuePair<string, string>("REm", "Ciasto têczowe z Espresso na miejscu"),
new KeyValuePair<string, string>("RPm", "Ciasto têczowe z Cappucino na miejscu"),
new KeyValuePair<string, string>("RLw", "Ciasto têczowe z Latte na wynos"),
new KeyValuePair<string, string>("RAw", "Ciasto têczowe z Americano na wynos"),
new KeyValuePair<string, string>("REw", "Ciasto têczowe z Espresso na wynos"),
new KeyValuePair<string, string>("RPw", "Ciasto têczowe z Cappucino na wynos")

};



    void Start()
    {
        //
        Customers = GameObject.Find("Customers");
        // Create a new instance of the Random class
        spawnPoint = GameObject.Find("Spawn").transform.position;
        spawnCount = 0;
        isSpawning = false;
        spawningRange = 1f;

    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ScenaMakowa")
        {
            if (!isSpawning && Customers.transform.childCount < 4)
            {
                isSpawning = true;
                StartCoroutine(Spawning());
            }
        }
        else if (SceneManager.GetActiveScene().name == "Kafelek")
        {
            if (!isSpawning && Customers.transform.childCount < 2)
            {
                isSpawning = true;
                StartCoroutine(Spawning());
            }
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

        else if (punkty.efficency == 0 && spawnCount < 2 && SceneManager.GetActiveScene().name == "Kafelek")
        {
            spawningRange = 1f;
        }

        else if (punkty.efficency == 0 && spawnCount < 4 && SceneManager.GetActiveScene().name == "ScenaMakowa")
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
