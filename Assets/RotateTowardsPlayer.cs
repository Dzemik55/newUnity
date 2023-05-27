using UnityEngine;
using UnityEngine.UI;

public class RotateTowardsPlayer : MonoBehaviour
{
    public Slider CierpliwoscSlider;
    float lerpValue;

    Navmesh_mak obj;
    float timeElapsed;



    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    private void Update()
    {
        ChangeSlider();
    }

    public void ChangeSlider()
    {
        if (obj != null)
        {
            lerpValue = obj.currentPatienceValue / 100f;
            CierpliwoscSlider.value = Mathf.Lerp(obj.minPatienceValue, obj.maxPatienceValue, lerpValue);
            CierpliwoscSlider.fillRect.GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, lerpValue);
            timeElapsed += Time.deltaTime;

            if (timeElapsed > obj.patienceDuration)
            {
                float remainingTime = timeElapsed - obj.patienceDuration;
                obj.currentPatienceValue = Mathf.Lerp(obj.maxPatienceValue, obj.minPatienceValue, Mathf.InverseLerp(0f, obj.patienceDuration, remainingTime));
            }
            else
            {
                obj.currentPatienceValue = 100;
            }
        }
    }


}
