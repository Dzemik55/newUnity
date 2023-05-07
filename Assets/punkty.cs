using UnityEngine;
using TMPro;

public class punkty : MonoBehaviour
{
    public static float score = 0;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.text = "Punkty: " + score.ToString(); // oryginalna wartoœæ
    }

    private void Update()
    {

        text.text = "Punkty: " + Mathf.RoundToInt(score).ToString();
    }
}