using UnityEngine;
using UnityEngine.UI;

public class RotateTowardsPlayer : MonoBehaviour
{
    float lerpValue;

    Navmesh_mak obj;
    float timeElapsed;



    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

 

    


}
