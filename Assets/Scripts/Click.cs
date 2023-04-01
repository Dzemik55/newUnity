using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Transform cloneObj;
    public int foodValue;
    public static Vector3 spawnPosition = new Vector3(-0.0152646303f, -2.17700005f, 5.16599989f);

    void Start()
    {
        spawnPosition.y += + 0.025f;
        Debug.Log(Click.spawnPosition.y);
    }


    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "Bulka_dol")
        {
            Instantiate(cloneObj, new Vector3(-0.0152646303f, spawnPosition.y, 5.16599989f), cloneObj.rotation);
            spawnPosition.y += 0.025f;
            Debug.Log(Click.spawnPosition.y);
        }
        if (gameObject.name == "Bulka_gora")
        {
            Instantiate(cloneObj, new Vector3(-0.0152646303f, spawnPosition.y, 5.16599989f), cloneObj.rotation);
            spawnPosition.y += 0.025f;
            Debug.Log(Click.spawnPosition.y);
        }
        if (gameObject.name == "Ser") 
        { 
            Instantiate(cloneObj, new Vector3(-0.0152646303f, spawnPosition.y, 5.16599989f), cloneObj.rotation);
            spawnPosition.y += 0.025f;
            Debug.Log(Click.spawnPosition.y);
        }
        if (gameObject.name == "Bekon")
        {
            Instantiate(cloneObj, new Vector3(-0.0152646303f, spawnPosition.y, 5.16599989f), cloneObj.rotation);
            spawnPosition.y += 0.025f;
            Debug.Log(Click.spawnPosition.y);
        }

        if (gameObject.name == "Kotlet")
        {
            Instantiate (cloneObj, new Vector3(0.5f, -2.197392f+0.1f, 4.71999979f), cloneObj.rotation);
        }

        GameFlow.plateValue += foodValue;
        Debug.Log(GameFlow.plateValue + " " + GameFlow.orderValue);
    }
}
