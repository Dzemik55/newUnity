using UnityEngine;

public class PickUpKnife : MonoBehaviour
{
    public Camera playerCamera;
    public static bool nozPodniesiony = false;
    public static bool nozOdlozony = true;
    void Start()
    {

    }



    // Update is called once per frame
    void OnMouseDown()
    {
        if (!nozPodniesiony && nozOdlozony)
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Nó¿ trafiony");
                Transform clickedObject = hit.transform;
                Transform parentObject = GameObject.Find("First Person Camera").transform; // Obiekt, który ma zostaæ nowym rodzicem
                clickedObject.SetParent(parentObject);
                Vector3 newPosition = new Vector3(0.583999991f, -0.321999997f, 0.819999993f);
                Quaternion newRotation = new Quaternion(-0.867192209f, 0.0977871269f, -0.0817015246f, 0.481394112f);
                clickedObject.GetComponent<Rigidbody>().useGravity = false;
                clickedObject.GetComponent<Rigidbody>().isKinematic = true;
                clickedObject.GetComponent<MeshCollider>().enabled = false;
                clickedObject.transform.localRotation = newRotation;
                clickedObject.transform.localPosition = newPosition;


                clickedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

                nozPodniesiony = true;
                nozOdlozony = false;
            }
        }
    }


}
