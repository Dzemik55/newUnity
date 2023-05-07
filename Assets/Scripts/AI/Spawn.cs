using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Customers;
    public GameObject spawn;
    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        Customers = GameObject.Find("Customers");
    }

    // Update is called once per frame
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