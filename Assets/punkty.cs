using TMPro;
using UnityEngine;

public class punkty : MonoBehaviour
{
    public static float efficency = 0;
    public static float score = 0;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.text = "Punkty: " + score.ToString(); // oryginalna wartoœæ
    }

    private void Start()
    {
        score = 0;
        efficency = 0;
    }

    private void Update()
    {

        text.text = "Punkty: " + Mathf.RoundToInt(score).ToString();
    }
}