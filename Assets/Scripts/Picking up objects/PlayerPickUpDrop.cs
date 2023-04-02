using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickUpDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;



    private ObjectGrabbable objectGrabbable;

    // Update is called once per frame

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                float pickUpDistance = 3f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {

                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                        objectGrabbable.GetComponent<Rigidbody>().angularDrag = 5;
                        objectGrabbable.GetComponent<Rigidbody>().drag = 5;
                        objectGrabbable.GetComponent<Rigidbody>().freezeRotation = true;
                    }

                }
            }
            else
            {
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }
}

