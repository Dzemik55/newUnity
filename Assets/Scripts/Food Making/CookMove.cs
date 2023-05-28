using System.Collections;
using System.Drawing;
using UnityEngine;

public class CookMove : MonoBehaviour
{
    private MeshRenderer meatMaterial;
    private bool stillCooking = true;
    private bool isReady = false;
    private bool isBurned = false;
    public Material newMaterial;
    public Material burnedMaterial;
    public Texture newTexture;
    Click click;
    public Vector3 spawnPosition;
    public GameObject PatelniaNaKtorejLeze;
    public GameObject particlePrefab;
    void Start()
    {
        click = Click.instance;
        meatMaterial = GetComponent<MeshRenderer>();
        // newMaterial = Resources.Load<Material>("Patty");
        // newTexture = Resources.Load<Texture2D>("Patty");
        StartCoroutine(mainTimer());
    }


    void Update()
    {
    }

    private void OnMouseDown()
    {
        spawnPosition = click.plateObjects[click.currentPlateIndex].transform.position;
        float objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;
        if (click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().spawnHeight == 0f)
        {
            Debug.Log("hejka");
            float plateHeight = click.plateObjects[click.currentPlateIndex].GetComponent<Renderer>().bounds.size.y;
            click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().spawnHeight = plateHeight + Click.initialSpawnHeight;

        }
        else
        {
            click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().spawnHeight += objectHeight + click.spawnHeightOffset;
            //click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().spawnHeight = Click.spawnHeight;
        }
        spawnPosition += Vector3.up * click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().spawnHeight;
        GetComponent<Transform>().position = spawnPosition;
        GetComponent<Transform>().transform.parent = click.plateObjects[click.currentPlateIndex].transform;
        gameObject.GetComponent<PlacedIngredient>().enabled = true;
        GameObject smoke = gameObject.transform.Find("Smoke(Clone)").gameObject;
        smoke.SetActive(false);
        if (isReady && !isBurned)
        {
            if (gameObject.name.Contains("Bacon"))
            {
                click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters = click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters + "a";
            }
            if (gameObject.name.Contains("Roast"))
            {
                click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters = click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters + "K";
            }
            if (gameObject.name.Contains("Sausage"))
            {
                click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters = click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters + "s";
            }

            //Click.patelniaObjects[click.currentPatelniaIndex].GetComponent<IloscKotletow>().iloscKotletow = 0;
        }
        else
        {
            if (gameObject.name.Contains("Bacon"))
            {
                click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters = click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters + "A";
            }
            if (gameObject.name.Contains("Roast"))
            {
                click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters = click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters + "k";
            }
            if (gameObject.name.Contains("Sausage"))
            {
                click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters = click.plateObjects[click.currentPlateIndex].GetComponent<Plate>().plateLetters + "S";
            }
        }
        PatelniaNaKtorejLeze.GetComponent<IloscKotletow>().iloscKotletow = 0;
        PatelniaNaKtorejLeze = null;
        stillCooking = false;
        isReady = false;
    }


    IEnumerator mainTimer()
    {
        yield return StartCoroutine(cookTimer());
        yield return StartCoroutine(burnTimer());
    }
    IEnumerator cookTimer()
    {
        yield return new WaitForSeconds(10);
        if (stillCooking)
        {
            meatMaterial.material = newMaterial;
            // meatMaterial.material.mainTexture = newTexture;
            isReady = true;
            GameObject smoke = gameObject.transform.Find("Smoke(Clone)").gameObject;
            ParticleSystem.MainModule Smokeps = smoke.GetComponent<ParticleSystem>().main;
            UnityEngine.Color startColor = Smokeps.startColor.color;

            float brightnessFactor = 1.6f;
            startColor = UnityEngine.Color.grey;
            startColor.r *= brightnessFactor;
            startColor.g *= brightnessFactor;
            startColor.b *= brightnessFactor;
            startColor.a = 0.5f;
            Smokeps.startColor = startColor;
            Debug.Log("is Ready: " + isReady);
        }
    }

    IEnumerator burnTimer()
    {
        yield return new WaitForSeconds(10);
        if (stillCooking && isReady)
        {
            meatMaterial.material = burnedMaterial;
            // meatMaterial.material.mainTexture = newTexture;
            isBurned = true;
            GameObject smoke = gameObject.transform.Find("Smoke(Clone)").gameObject;
            ParticleSystem.MainModule Smokeps = smoke.GetComponent<ParticleSystem>().main;
            UnityEngine.Color startColor = Smokeps.startColor.color;
            startColor = UnityEngine.Color.black;
            startColor.a = 0.5f;
            Smokeps.startColor = startColor;

            Debug.Log("is Burned: " + isBurned);
        }
    }
}
