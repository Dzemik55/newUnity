using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Camera mainCamera;
    public bool isPickedUp = false;
    private Vector3 offset;
    public float distanceFromCamera = 2.0f; // distance between the camera and the object
    public float followSpeed = 10.0f;
    public float rotationSpeed = 5.0f; // speed of rotation
    private bool isRotating = false;
    private Quaternion targetRotation; // target rotation of the object

    public static GameObject currentObject; // Reference to the currently picked object


    void DestroyRigidbody()
    {
        Destroy(GetComponent<Rigidbody>());
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            if (!isPickedUp)
            {
                // Check if the mouse is pointing at this object
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        // Pick up the object
                        isPickedUp = true;
                        currentObject = gameObject; // Set the current object as the picked object

                        // Calculate the offset between the object and the camera
                        offset = transform.position - mainCamera.transform.position;
                        if (GetComponent<Rigidbody>() == null)
                        {
                            gameObject.AddComponent<Rigidbody>();
                        }
                        // Disable the object's physics while it's picked up
                        GetComponent<Rigidbody>().isKinematic = true;
                        Debug.Log("Current Object:" + currentObject.name);
                    }
                }
            }
            else
            {
                // Drop the object
                isPickedUp = false;
                currentObject = null; // Clear the reference to the picked object

                // Enable the object's physics

                GetComponent<Rigidbody>().isKinematic = false;
                Invoke("DestroyRigidbody", 0.5f);
                if (currentObject != null)
                {

                    Debug.Log("Current Object:" + currentObject.name);
                }
                else
                {
                    Debug.Log("No item is picked up right now");
                }
            }
        }

        if (isPickedUp)
        {
            // Calculate the position the object should move to
            Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

            // Move the object towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }

        if (isPickedUp && Input.GetKeyDown(KeyCode.E))
        {
            // Set the target rotation to rotate 90 degrees to the right
            targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * 90f);
            isRotating = true;
        }
        else if (isPickedUp && Input.GetKeyDown(KeyCode.Q))
        {
            // Set the target rotation to rotate 90 degrees to the left
            targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * -90f);
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Rotate the object towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Start()
    {
        distanceFromCamera = 1.5f;
        mainCamera = GameObject.Find("First Person Camera").GetComponent<Camera>();
    }

}
