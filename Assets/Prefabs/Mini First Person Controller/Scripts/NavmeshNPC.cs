using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshNPC : MonoBehaviour
{
    public GameObject WaypointsList;
    public List<GameObject> waypoints;
    public bool waitingInQueue;
    NavMeshAgent agent;
    public GameObject punkt;
    public GameObject obecnyPunkt = null;
    public int nrPunkt = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(this.name);
        waitingInQueue = false;
        WaypointsList = GameObject.Find("Queue");
        waypoints.Clear();
        foreach (Transform child in WaypointsList.transform)
        {
            if (child == null) { continue; }
            waypoints.Add(child.gameObject);
        }
        agent = GetComponent<NavMeshAgent>();
        punkt = waypoints[nrPunkt];
    }

    // Update is called once per frame
    void Update()
    {
        if (nrPunkt != 7)
        {
            StartCoroutine(Queue());
        }
        StopAllCoroutines();
       
        
    }
    IEnumerator Queue() 
    {
        if (waypoints[nrPunkt].GetComponent<isTaken>().Occupied != true)
        {
            punkt = waypoints[nrPunkt];
        }

        GameObject first = waypoints.First();
        GameObject last = waypoints.Last();
        if (punkt.GetComponent<isTaken>().Occupied == true && punkt == first)
        {
            Debug.Log(punkt.name + " zajety pierwszy");
            yield return null;
        }
        else if (punkt.GetComponent<isTaken>().Occupied == true)
        {
            Debug.Log(punkt.name + " zajety");
            yield return null;
        }
        else
        {
            Debug.Log(this.name + " - Idzie do: " + punkt);
            if (obecnyPunkt != null)
            {
                obecnyPunkt.GetComponent<isTaken>().Occupied = false;
            }
            agent.destination = punkt.transform.position;
            obecnyPunkt = punkt;
            punkt.GetComponent<isTaken>().Occupied = true;
            if (nrPunkt < waypoints.Count)
            {
                nrPunkt++;
            }
        }
        Debug.Log(this.name + " - Punkt: " + punkt + " Obecny punkt: " + obecnyPunkt);
    }
}



