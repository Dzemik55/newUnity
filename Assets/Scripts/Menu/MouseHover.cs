using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    TextMeshPro textmeshPro;
    // Start is called before the first frame update
    void Start()
    {
         textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.color = Color.black;

    }

    void OnMouseEnter()
    {
        textmeshPro.color=  Color.red;
    }

    void OnMouseExit()
    {
        textmeshPro.color = Color.black;
    }
}
