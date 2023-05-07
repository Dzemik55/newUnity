using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CheckAndMoveToFreePoint : MonoBehaviour
{
    public GameObject WaypointsList;
    public GameObject RegistersList;
    public GameObject ExitList;
    public bool isActive = true;
    public List<GameObject> exitPoints;
    public List<GameObject> registers;
    public List<GameObject> waypoints;
    private NavMeshAgent navMeshAgent;
    public GameObject currentRegister;
    private int lastCheckedIndex = -1;
    public bool isLeaving = false;

    private void Start()
    {
        WaypointsList = GameObject.Find("Queue");
        RegistersList = GameObject.Find("Register");
        ExitList = GameObject.Find("Exit");
        waypoints.Clear();
        exitPoints.Clear();
        foreach (Transform child in WaypointsList.transform)
        {
            if (child == null) { continue; }
            waypoints.Add(child.gameObject);
        }
        foreach (Transform child in RegistersList.transform)
        {
            if (child == null) { continue; }
            registers.Add(child.gameObject);
        }
        foreach (Transform child in ExitList.transform)
        {
            if (child == null) { continue; }
            exitPoints.Add(child.gameObject);
        }
        waypoints.Reverse();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if(!isLeaving) 
        {
            StartCoroutine(MoveToFreePoint());
        }
        
    }
    private void Update()
    {
        if (!isActive)
        {
            
        }
    }
    private IEnumerator MoveToFreePoint()
    {
        while (true)
        {
            bool foundFreePoint = false;
            if (!isActive)
            {
                break;
            }

            // Sprawdzenie, czy istnieje wolna kasa
            for (int i = registers.Count - 1; i >= 0; i--)
            {
                isTaken isTakenComponent = registers[i].GetComponent<isTaken>();
                if (isTakenComponent != null && !isTakenComponent.Occupied && !isLeaving && isActive)
                {
                    isTakenComponent.Occupied = true;
                    currentRegister = registers[i];
                    navMeshAgent.SetDestination(registers[i].transform.position);
                    yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);
                    yield return StartCoroutine(WaitForInteraction()); // Wait for player interaction
                    foundFreePoint = true;
                    break; // Move to the first free register only
                }
            }

            // Sprawdzenie, czy w kolejce jest wolne miejsce
            if (!foundFreePoint && !isLeaving && isActive)
            {
                bool foundFreeQueuePoint = false;
                for (int i = 0; i < waypoints.Count; i++)
                {
                    isTaken isTakenComponent = waypoints[i].GetComponent<isTaken>();
                    if (isTakenComponent != null && !isTakenComponent.Occupied)
                    {
                        isTakenComponent.Occupied = true;
                        navMeshAgent.SetDestination(waypoints[i].transform.position);
                        yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);
                        yield return new WaitForSeconds(1f); // Wait for 1 second at the destination
                        isTakenComponent.Occupied = false;
                        foundFreeQueuePoint = true;
                        break; // Move to the first free queue point only
                    }
                }
                // If there are no free queue points, wait for some time before checking again
                if (!foundFreeQueuePoint)
                {
                    yield return new WaitForSeconds(5f);
                }
            }

            if (isLeaving)
            {
                currentRegister.GetComponent<isTaken>().Occupied = false; // Free the register
                                                                          // Move to each exit point and leave the building
                foreach (GameObject exitPoint in exitPoints)
                {
                    navMeshAgent.SetDestination(exitPoint.transform.position);
                    yield return new WaitUntil(() => !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance);

                    // Wait for some time before moving to the next exit point
                    yield return new WaitForSeconds(1f);
                }

                // Reset the current register and disable the agent
                isActive = false;
                isLeaving = false;
                Destroy(gameObject);
            }
        }
    }


    private IEnumerator WaitForInteraction()
    {
        // Wait for player to look at the agent and press F
        while (true)
        {
            // Check if the player is looking at the agent
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraRay, out hit) && hit.collider.gameObject == gameObject)
            {
                // Player is looking at the agent, wait for F key press
                isLeaving = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    break;
                }
            }
            else
            {
                // Player is not looking at the agent, don't wait for interaction
                isLeaving = false;
            }
            yield return null;
        }
    }
}