using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour
{
    TextMeshProUGUI textmeshPro;
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        textmeshPro = GetComponent<TextMeshProUGUI>();
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
