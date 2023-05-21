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


    private void OnMouseDown()
    {
        if (!isPickedUp)
        {
            // Pick up the object
            isPickedUp = true;

            // Calculate the offset between the object and the camera
            offset = transform.position - mainCamera.transform.position;

            // Disable the object's physics while it's picked up
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            // Drop the object
            isPickedUp = false;

            // Enable the object's physics
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnMouseUp()
    {
        // Drop the object
        isPickedUp = false;

        // Enable the object's physics
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void FixedUpdate()
    {
        if (isPickedUp)
        {
            // Calculate the position the object should move to
            Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * distanceFromCamera;

            // Move the object towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (isPickedUp && Input.GetMouseButton(0)) // Only rotate object if it's picked up and mouse button is held down
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Set the target rotation to rotate 90 degrees to the right
                targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * 90f);
                isRotating = true;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                // Set the target rotation to rotate 90 degrees to the left
                targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * -90f);
                isRotating = true;
            }
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
}
