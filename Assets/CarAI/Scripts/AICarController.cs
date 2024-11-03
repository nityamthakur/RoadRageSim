using UnityEngine;

public class AICarController : MonoBehaviour
{
    public Transform[] waypoints;       // Array of waypoints
    public float speed = 5f;            // Movement speed
    public float reachThreshold = 1f;   // Distance threshold to reach a waypoint

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Check if there are waypoints set up
        if (waypoints.Length == 0) return;

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;
        
        // Move in the direction of the waypoint
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // Rotate to face the direction of movement
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);

        // Check if AI car has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < reachThreshold)
        {
            // Move to the next waypoint
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;  // Reset to start or remove to stop at end
            }
        }
    }
}