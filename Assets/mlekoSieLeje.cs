using UnityEngine;

public class mlekoSieLeje : MonoBehaviour
{
    private PickUp pickUpScript;
    private Quaternion targetRotation;
    private bool isRotating = false;
    public float rotationSpeed = 5.0f;
    public ParticleSystem mleko;


    private void Start()
    {
        // Get reference to the PickUp script attached to the same game object as this script
        pickUpScript = GetComponent<PickUp>();
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        if (pickUpScript.isPickedUp && Input.GetMouseButton(1)) // Only rotate object if it's picked up and right mouse button is held down
        {


            // Set the target rotation to rotate X to 0
            targetRotation = Quaternion.Euler(-144.232f, -89.97501f, 447.314f);
            isRotating = true;
            mleko.Play();


        }
        else
        {
            targetRotation = Quaternion.Euler(-90, 0, 357.328f);
            isRotating = false;
            mleko.Stop();
            //stop movig mlekoEffect
        }

        if (isRotating)
        {
            // Rotate the object towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
