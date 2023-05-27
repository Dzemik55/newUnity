using UnityEngine;

public class Kawka : MonoBehaviour
{
    public string Kawa = "";
    public int iloscCukru = 0;
    public int iloscMleka = 0;
    public bool naWynos = false;
    public string kawaValue = "";

    private void Start()
    {
        if (naWynos && Kawa == "Latte")
        {
            kawaValue = "Lw";
        }
        if (naWynos && Kawa == "Americano")
        {
            kawaValue = "Aw";
        }
        if (naWynos && Kawa == "Espresso")
        {
            kawaValue = "Ew";
        }
        if (naWynos && Kawa == "Cappucino")
        {
            kawaValue = "Pw";
        }
        if (!naWynos && Kawa == "Latte")
        {
            kawaValue = "Lm";
        }
        if (!naWynos && Kawa == "Americano")
        {
            kawaValue = "Am";
        }
        if (!naWynos && Kawa == "Espresso")
        {
            kawaValue = "Em";
        }
        if (!naWynos && Kawa == "Cappucino")
        {
            kawaValue = "Pm";
        }

    }

}
