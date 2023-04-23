using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(Spawning());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Spawning()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2.0f);
            GameObject spawned = Instantiate(spawn, new Vector3(-3.759f, 0, -23.27f), Quaternion.identity);
            spawned.name = spawned.name + i;
            yield return new WaitForSeconds(2.0f);
        }




    }
}
