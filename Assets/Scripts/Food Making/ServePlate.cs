using UnityEngine;

public class ServePlate : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (GameFlow.orderValue == GameFlow.plateValue)
        {
            Debug.Log("correct");
            Click.spawnPosition.y = -2.17700005f + 0.03f;
            Debug.Log(Click.spawnPosition.y);
        }
    }
}
