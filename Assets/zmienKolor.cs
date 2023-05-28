using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zmienKolor : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Kawka>().Kawa == "Americano" && GetComponent<Kawka>().naWynos == true)
        {
            gameObject.transform.Find("Sleeve").GetComponent<MeshRenderer>().material = Resources.Load<Material>("Americano");
        }
        if (GetComponent<Kawka>().Kawa == "Cappucino" && GetComponent<Kawka>().naWynos == true)
        {
            gameObject.transform.Find("Sleeve").GetComponent<MeshRenderer>().material = Resources.Load<Material>("Cappucino");
        }
        if (GetComponent<Kawka>().Kawa == "Espresso" && GetComponent<Kawka>().naWynos == true)
        {
            gameObject.transform.Find("Sleeve").GetComponent<MeshRenderer>().material = Resources.Load<Material>("Espresso");
        }
        if (GetComponent<Kawka>().Kawa == "Latte" && GetComponent<Kawka>().naWynos == true)
        {
            gameObject.transform.Find("Sleeve").GetComponent<MeshRenderer>().material = Resources.Load<Material>("Latte");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
