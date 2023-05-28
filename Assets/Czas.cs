using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Czas : MonoBehaviour
{
    public float totalTime = 5 * 60; // Czas w sekundach - 5 minut
    private float currentTime;
    private TMP_Text tekst;

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        tekst = GetComponent<TMP_Text>();
        tekst.outlineColor = Color.black;
        tekst.outlineWidth = 1;
        currentTime = totalTime;
      //  textOutline.effectColor = Color.black;
       // textOutline.effectDistance = new Vector2(1f, -1f);
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        // Sprawdzamy, czy czas siê skoñczy³
        if (currentTime <= 0)
        {
            currentTime = 0;
            Time.timeScale = 0f;
            GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>().canRotate = false;
            transform.parent.transform.Find("Panel").gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            // Tutaj mo¿na wykonaæ jak¹œ akcjê po zakoñczeniu czasu
        }

        // Formatowanie czasu do formatu MM:SS
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timeString = "Czas: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        // Aktualizacja tekstu w komponencie Text
        tekst.text = timeString;

        // Zmiana koloru tekstu, gdy pozosta³y czas jest poni¿ej jednej minuty
        if (currentTime <= 60)
        {
            // Zmiana koloru tekstu co sekundê
            if (Mathf.FloorToInt(currentTime) % 2 == 0)
            {
                tekst.color = Color.red;
            }
            else
            {
                tekst.color = Color.white;
            }
        }
        else
        {
            // Przywrócenie domyœlnego koloru tekstu
            tekst.color = Color.white;
        }
    }

}
