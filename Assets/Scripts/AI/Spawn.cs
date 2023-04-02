using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawn, new Vector3(5, -2.98499775f, 14), Quaternion.identity).AddComponent<Navigation>();
        Instantiate(spawn, new Vector3(5, -2.98499775f, 12), Quaternion.identity).AddComponent<Navigation>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
