using UnityEngine;
using System.Collections;

public class MoveBetweenWaypoints0 : MonoBehaviour
{
    public Transform waypointA; // Starting waypoint
    public Transform waypointB; // Ending waypoint
    public float speed = 10f; // Movement speed
    public float waitTime = 5f; // Time to wait at each waypoint

    private Transform targetWaypoint; // Current target waypoint
    private bool isWaiting = false; // To check if the car is waiting

    void Start()
    {
        // Start by moving toward WaypointA
        targetWaypoint = waypointA;
    }

    void Update()
    {
        // Move toward the target if not waiting
        if (!isWaiting)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        // Move the car toward the target waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Check if the car has reached the target waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Start the wait coroutine and switch the waypoint
            StartCoroutine(WaitAndSwitch());
        }
    }

    IEnumerator WaitAndSwitch()
    {
        isWaiting = true; // Set waiting to true
        yield return new WaitForSeconds(waitTime); // Wait for the specified time
        // Switch the target waypoint
        targetWaypoint = (targetWaypoint == waypointA) ? waypointB : waypointA;
        isWaiting = false; // Set waiting to false
    }
}