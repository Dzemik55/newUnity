using UnityEngine;

public class PickUpKnife : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject yourItemPrefab;
    public bool nozPodniesiony = false;
    public bool nozOdlozony = true;
    void Start()
    {

    }

    // Update is called once per frame
    void OnMouseDown()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Nó¿ trafiony");
            Transform clickedObject = hit.transform;
            Transform parentObject = GameObject.Find("First Person Camera").transform; // Obiekt, który ma zostaæ nowym rodzicem
            //clickedObject.SetParent(parentObject);
            Vector3 newPosition = new Vector3(0.352999985f, -0.181999996f, 0.504000008f);
            Vector3 newRotation = new Vector3(273.758057f, 180f, 180f);
            clickedObject.transform.position = newPosition;
            /*clickedObject.transform.rotation = Quaternion.*/
            clickedObject.GetComponent<Rigidbody>().useGravity = false;
            clickedObject.GetComponent<Rigidbody>().isKinematic = true;

            clickedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;

            nozPodniesiony = true;
            nozOdlozony = false;
        }
    }


}
