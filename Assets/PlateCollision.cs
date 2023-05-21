using UnityEngine;

public class PlateCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ingredient" || collision.gameObject.tag == "Plate")
        {
            // Get the rigidbody component
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            // Set the kinematic property to false to enable physics simulation
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
    }
}
