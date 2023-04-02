using System.Collections;
using UnityEngine;

public class CookMove : MonoBehaviour
{
    private int foodValue = 0;
    private MeshRenderer meatMaterial;
    private bool stillCooking = true;
    Material newMaterial;
    Texture newTexture;
    void Start()
    {
        meatMaterial = GetComponent<MeshRenderer>();
        newMaterial = Resources.Load<Material>("Patty");
        newTexture = Resources.Load<Texture2D>("Patty");
        StartCoroutine(cookTimer());
    }


    void Update()
    {

    }

    private void OnMouseDown()
    {
        GetComponent<Transform>().position = new Vector3(-0.0152646303f, Click.spawnPosition.y, 5.16599989f);
        Click.spawnPosition.y += 0.025f;
        GameFlow.plateValue += foodValue;
        stillCooking = false;
    }

    IEnumerator cookTimer()
    {
        yield return new WaitForSeconds(10);
        foodValue = 1000;
        if (stillCooking)
        {
            meatMaterial.material = newMaterial;
            meatMaterial.material.mainTexture = newTexture;
        }
    }
}
