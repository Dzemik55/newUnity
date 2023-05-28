using System.Collections.Generic;
using UnityEngine;

public class RespawnCake : MonoBehaviour
{
    Transform platePosition;
    Transform plateChild;
    public GameObject newObject;
    public GameObject newCake;
    private Vector3 startingPosition;

    private List<TransformData> childTransformData = new List<TransformData>();

    private void Start()
    {
        platePosition = transform;
        startingPosition = transform.position;
        plateChild = transform.GetChild(0);

        // Store child positions and rotations
        StoreChildTransformData();
    }

    private void StoreChildTransformData()
    {
        childTransformData.Clear();
        foreach (Transform child in plateChild)
        {
            TransformData transformData = new TransformData(child.position, child.rotation);
            childTransformData.Add(transformData);
        }
    }

    private void Update()
    {
        int childCount = plateChild.childCount;
        if (childCount == 0)
        {
            gameObject.transform.position = startingPosition;

            // Check if the GameObject name is "Muffin box"
            if (gameObject.name == "Muffin box")
            {
                // Get the Muffins script component from the game object
                Muffins muffinsScript = GetComponent<Muffins>();

                if (muffinsScript != null && muffinsScript.muffins.Length > 0)
                {
                    // Respawn children with stored positions and rotations
                    for (int i = 0; i < childTransformData.Count; i++)
                    {
                        TransformData transformData = childTransformData[i];

                        // Randomly choose a muffin prefab from the muffins array in Muffins script
                        int randomIndex = Random.Range(0, muffinsScript.muffins.Length);
                        newObject = muffinsScript.muffins[randomIndex];

                        GameObject newSpawnedMuffin = Instantiate(newObject, transformData.position, transformData.rotation);

                        newSpawnedMuffin.transform.SetParent(plateChild);
                        newSpawnedMuffin.transform.localScale = Vector3.one;
                    }
                }
            }
            else
            {
                // Respawn children with stored positions and rotations using the default newCake prefab
                for (int i = 0; i < childTransformData.Count; i++)
                {
                    TransformData transformData = childTransformData[i];
                    GameObject newSpawnedCake = Instantiate(newCake, transformData.position, transformData.rotation);
                    newSpawnedCake.transform.SetParent(plateChild);
                }
            }
        }
    }


    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
