using UnityEngine;

public class OdlozNoz : MonoBehaviour
{
    public Camera playerCamera;
    public static bool nozPodniesiony = false;
    public static bool nozOdlozony = true;

    void Update()
    {
        if (!nozPodniesiony && nozOdlozony)
        {
            PodniesNoz();
        }
        else
        {
            OdlozNozyk();
        }
    }

    public void OdlozNozyk()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag != "Kawa" && hit.collider.gameObject.tag != "Ingredient" && hit.collider.gameObject.GetComponent<PickUp>() == null && hit.collider.gameObject.GetComponent<MakeACoffe>() == null)
                {
                    Vector3 itemPosition = hit.point + new Vector3(0f, 0.5f, 0f);
                    Transform knifeTransform = playerCamera.transform.Find("knife");

                    if (knifeTransform != null)
                    {
                        GameObject knife = knifeTransform.gameObject;
                        knife.transform.parent = null;
                        knife.transform.position = itemPosition;
                        knife.GetComponent<Rigidbody>().useGravity = true;
                        knife.GetComponent<Rigidbody>().isKinematic = false;
                        knife.GetComponent<BoxCollider>().enabled = true;
                        knife.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        knife.transform.localScale = new Vector3(0.0300099328f, 0.0300102904f, 0.024555305f);
                        nozPodniesiony = false;
                        nozOdlozony = true;
                        Debug.Log("Nó¿ od³o¿ony: " + nozOdlozony);
                    }
                }
            }
        }
    }

    public void PodniesNoz()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "knife")
                {
                    // Calculate the distance between the knife and the player
                    float distanceToPlayer = Vector3.Distance(transform.position, playerCamera.transform.position);

                    if (distanceToPlayer <= 3f)
                    {
                        Debug.Log("Nó¿ trafiony");
                        Transform clickedObject = hit.transform;
                        Transform parentObject = GameObject.Find("First Person Camera").transform; // Obiekt, który ma zostaæ nowym rodzicem
                        clickedObject.SetParent(parentObject);
                        Vector3 newPosition = new Vector3(0.583999991f, -0.321999997f, 0.819999993f);
                        Quaternion newRotation = new Quaternion(-0.867192209f, 0.0977871269f, -0.0817015246f, 0.481394112f);
                        clickedObject.GetComponent<Rigidbody>().useGravity = false;
                        clickedObject.GetComponent<Rigidbody>().isKinematic = true;
                        clickedObject.GetComponent<BoxCollider>().enabled = false;
                        clickedObject.transform.localRotation = newRotation;
                        clickedObject.transform.localPosition = newPosition;
                        clickedObject.transform.localScale = new Vector3(0.0300099328f, 0.0300102904f, 0.024555305f);

                        clickedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

                        nozPodniesiony = true;
                        nozOdlozony = false;
                        Debug.Log("Nó¿ od³o¿ony: " + nozPodniesiony);
                    }
                    else
                    {
                        Debug.Log("Nó¿ jest zbyt daleko, nie mo¿na go podnieœæ.");
                    }
                }
            }
        }
    }
}
