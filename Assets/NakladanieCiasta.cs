using UnityEngine;

public class NakladanieCiasta : MonoBehaviour
{
    public Vector3 spawnPosition;
    public static float initialSpawnHeight = 0.1f;
    public string literkaCiasta = "";
    public string literkaKawy = "";
    public int iloscMleka = 0;
    public int iloscCukru = 0;
    public PlateManager plates;


    private void OnMouseEnter()
    {
        if (OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony && tag == "Ingredient")
        {
            GetComponent<Outline>().enabled = true;
        }
        else if (!OdlozNoz.nozPodniesiony && OdlozNoz.nozOdlozony && tag == "Kawa")
        {
            GetComponent<Outline>().enabled = true;

        }

        if (tag == "Kawa")
        {

            literkaKawy = GetComponent<Kawka>().kawaValue;
            iloscCukru = GetComponent<Kawka>().iloscCukru;
            iloscMleka = GetComponent<Kawka>().iloscMleka;
        }
    }

    private void Start()
    {
        plates = GameObject.Find("PlateManagment").GetComponent<PlateManager>();
    }



    private void OnMouseOver()
    {
        float minimalnaOdleglosc = 0.56007f;
        float maksymalnaOdleglosc = 2.604455f;
        if (Input.GetMouseButtonDown(1) && OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony && tag == "Ingredient")
        {

            if (plates.currentPlateIndex >= plates.plateObjects.Length)
            {
                Debug.LogWarning("No plate object available for the current index.");
                return;
            }

            GameObject currentPlate = plates.plateObjects[plates.currentPlateIndex];
            float distance = Vector3.Distance(transform.position, currentPlate.transform.position);
            if (distance <= minimalnaOdleglosc || distance >= maksymalnaOdleglosc)
            {
                Debug.Log("Odleg³oœæ nie mieœci siê w zakresie.");
                return;
            }
            else
            {
                Debug.Log("Odleg³oœæ mieœci siê w zakresie.");

                if (currentPlate.transform.childCount > 0)
                {
                    int objectCountOnPlate = currentPlate.transform.childCount;

                    // Check if the plate already has an object
                    if (objectCountOnPlate > 1)
                    {
                        Debug.LogWarning("There is already an object on the plate.");
                        return;
                    }

                }


                spawnPosition = currentPlate.transform.position;

                // Calculate the height of the object
                float objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;

                // Calculate the center position of the plate
                Vector3 plateCenter = currentPlate.GetComponent<Renderer>().bounds.center;

                // Set the spawn position slightly above the center of the plate
                /* spawnPosition = plateCenter + Vector3.up * objectHeight * 0.8f;*/
                spawnPosition = new Vector3(0, 0.157f, -0.002f);
                string rodzajKawy = "";
                if (GetComponent<Kawka>() != null)
                {
                    rodzajKawy = GetComponent<Kawka>().Kawa;
                }

                if (rodzajKawy == "Americano" || rodzajKawy == "Cappucino")
                {
                    spawnPosition = new Vector3(0, 0.0140000004f, 0.112999998f);
                }
                if (rodzajKawy == "Espresso")
                {
                    spawnPosition = new Vector3(-0.0890000015f, 0.101000004f, 0.112999998f);
                }
                if (rodzajKawy == "Latte")
                {
                    spawnPosition = new Vector3(0, 0.0410000011f, 0.108999997f);
                }
                if (GetComponent<Kawka>() != null)
                    if (GetComponent<Kawka>().naWynos == true)
                    {
                        spawnPosition = new Vector3(0, 0.136999995f, 0.156000003f);
                        //transform.localScale = new Vector3(0.0388443582f, 0.0388443582f, 0.0602052398f);
                    }
                Vector3 scale = transform.localScale;
                Quaternion rotation = transform.rotation;

                transform.SetParent(null);
                transform.SetParent(currentPlate.transform);
                transform.localPosition = spawnPosition;
                if (GetComponent<Kawka>() != null)
                    if (GetComponent<Kawka>().naWynos == false)
                    {
                        transform.localScale = scale;
                    }

                transform.localRotation = rotation;
                if (tag == "Ingredient")
                {
                    transform.rotation = Quaternion.identity;
                    transform.localScale = scale;
                }
                string aktualneLiterkiTalerza = plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters;
                if (literkaKawy == "" && literkaCiasta != "")
                {
                    plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters = literkaCiasta + aktualneLiterkiTalerza;

                }
                else if (literkaKawy != "" && literkaCiasta == "")
                {
                    plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters = aktualneLiterkiTalerza + literkaKawy;
                    plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateMilk = iloscMleka;
                    plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateSugar = iloscCukru;
                }
                GetComponent<Outline>().enabled = false;
            }
        }
        else if (Input.GetMouseButtonDown(0) && !OdlozNoz.nozPodniesiony && OdlozNoz.nozOdlozony && tag == "Kawa")
        {
            if (plates.currentPlateIndex >= plates.plateObjects.Length)
            {
                Debug.LogWarning("No plate object available for the current index.");
                return;
            }

            GameObject currentPlate = plates.plateObjects[plates.currentPlateIndex];
            if (currentPlate.transform.childCount > 0)
            {
                int objectCountOnPlate = currentPlate.transform.childCount;

                // Check if the plate already has an object
                if (objectCountOnPlate > 1)
                {
                    Debug.LogWarning("There is already an object on the plate.");
                    return;
                }

            }


            spawnPosition = currentPlate.transform.position;

            // Calculate the height of the object
            float objectHeight = gameObject.GetComponent<Renderer>().bounds.size.y;

            // Calculate the center position of the plate
            Vector3 plateCenter = currentPlate.GetComponent<Renderer>().bounds.center;

            // Set the spawn position slightly above the center of the plate
            /* spawnPosition = plateCenter + Vector3.up * objectHeight * 0.8f;*/
            spawnPosition = new Vector3(0, 0.157f, -0.002f);
            string rodzajKawy = "";
            if (GetComponent<Kawka>() != null)
            {
                rodzajKawy = GetComponent<Kawka>().Kawa;
            }

            if (rodzajKawy == "Americano" || rodzajKawy == "Cappucino")
            {
                spawnPosition = new Vector3(0, 0.0140000004f, 0.112999998f);
            }
            if (rodzajKawy == "Espresso")
            {
                spawnPosition = new Vector3(-0.0890000015f, 0.101000004f, 0.112999998f);
            }
            if (rodzajKawy == "Latte")
            {
                spawnPosition = new Vector3(0, 0.0410000011f, 0.108999997f);
            }
            if (GetComponent<Kawka>() != null)
                if (GetComponent<Kawka>().naWynos == true)
                {
                    spawnPosition = new Vector3(0, 0.136999995f, 0.156000003f);
                    //transform.localScale = new Vector3(0.0388443582f, 0.0388443582f, 0.0602052398f);
                }
            Vector3 scale = transform.localScale;
            Quaternion rotation = transform.rotation;

            transform.SetParent(null);
            transform.SetParent(currentPlate.transform);
            transform.localPosition = spawnPosition;
            if (GetComponent<Kawka>() != null)
                if (GetComponent<Kawka>().naWynos == false)
                {
                    transform.localScale = scale;
                }

            transform.localRotation = rotation;
            if (tag == "Ingredient")
            {
                transform.rotation = Quaternion.identity;
                transform.localScale = scale;
            }
            string aktualneLiterkiTalerza = plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters;
            if (literkaKawy == "" && literkaCiasta != "")
            {
                plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters = literkaCiasta + aktualneLiterkiTalerza;

            }
            else if (literkaKawy != "" && literkaCiasta == "")
            {
                plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateLetters = aktualneLiterkiTalerza + literkaKawy;
                plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateMilk = iloscMleka;
                plates.plateObjects[plates.currentPlateIndex].GetComponent<Plate>().plateSugar = iloscCukru;
            }
            GetComponent<Outline>().enabled = false;
        }
    }


    private void OnMouseExit()
    {
        if (OdlozNoz.nozPodniesiony && !OdlozNoz.nozOdlozony)
        {
            GetComponent<Outline>().enabled = false;
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