using UnityEngine;

public class RespawnCake : MonoBehaviour
{
    Transform platePosition;
    Transform PlateChild;
    public int ChildChildrenCount = 0;
    public GameObject newObject;
    public GameObject newCake;
    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private void Start()
    {
        platePosition = gameObject.transform;
        startingPosition = transform.position;
        startingRotation = transform.rotation;
        PlateChild = gameObject.transform.GetChild(0);
    }

    private void Update()
    {
        ChildChildrenCount = PlateChild.childCount;
        if (PlateChild.childCount == 0)
        {
            Destroy(gameObject);
            Instantiate(newObject, startingPosition, startingRotation);
            newCake.transform.position = PlateChild.transform.position;
            newCake.transform.rotation = PlateChild.transform.rotation;
            GameObject newSpawnedCake = Instantiate(newCake, newCake.transform.position, newCake.transform.rotation);
            newSpawnedCake.transform.SetParent(gameObject.transform);
        }
    }
}
