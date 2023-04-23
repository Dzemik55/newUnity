using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    [SerializeField] GameObject Destination;
    [SerializeField] GameObject WaypointsList;
    public List<GameObject> waypoints;
    public bool isSeated;
    bool seatFound;
    int i = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        isSeated = false;
        WaypointsList = GameObject.Find("Waypoints");
        foreach (Transform child in WaypointsList.transform)
        {
            waypoints.Add(child.gameObject);
        }
        Destination = waypoints[i];
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSeated)
        {

            transform.position = Vector3.MoveTowards(transform.position, Destination.transform.position, 0.01f);
            if (Vector3.Distance(transform.position, Destination.transform.position) < 0.001f && seatFound == true)
            {
                isSeated = true;
            }
            if (Vector3.Distance(transform.position, Destination.transform.position) < 0.001f)
            {
                Debug.Log("Iloœæ miejsc przy stole:" + Destination.transform.childCount);
                if (Destination.transform.childCount > 0)
                {
                    GameObject miejsce = Destination.transform.Find("Miejsce").gameObject;
                    Debug.Log("Miejsce zajête:" + miejsce.GetComponent<isTaken>().Occupied);
                    if (miejsce.GetComponent<isTaken>().Occupied == false)
                    {
                        Debug.Log(miejsce.transform.position);
                        Debug.Log(transform.position);
                        transform.LookAt(miejsce.transform);
                        Destination = miejsce;
                        seatFound = true;
                        miejsce.GetComponent<isTaken>().Occupied = true;


                    }
                    else
                    {
                        if (i < waypoints.Count)
                            Destination = waypoints[i++];
                    }
                }

                else
                {
                    if (i < waypoints.Count)
                        Destination = waypoints[i++];
                }

            }
        }

    }
}
