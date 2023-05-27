using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odleglosc : MonoBehaviour
{
    GameObject deska;

    private void Start()
    {
        deska = GameObject.Find("cutting board");

        float distance = Vector3.Distance(deska.transform.position, gameObject.transform.position);
        Debug.Log("Odleg�o�� mi�dzy desk� a innym tym talerzem: " + distance);
    }
}

