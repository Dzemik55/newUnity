using UnityEngine;

public class Dym : MonoBehaviour
{
    public ParticleSystem para;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            para.Play();
        }
    }
}
