using System.Collections;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    // Add a public GameObject for the Jail waypoint
    public GameObject jailWaypoint;

    // Add a boolean to track if the player is in jail
    public bool isInJail = false;

    [Tooltip("Tip: Press the small lock in the top right corner to drag multiple waypoints into the list.")]
    public GameObject[] waypoints;

    [Tooltip("Should the object repeat the path?")]
    public bool loop;
    [Tooltip("How fast should the object move?")]
    public float speed = 10.0f;

    [Header("")]
    [Header("(optional):")]

    [Header("Rotate to Target Settings")]
    [Tooltip("Here you can change whether this object should rotate towards the object.")]
    public bool rotateToTarget;
    [Tooltip("Rotation Speed")]
    public float rotSpeed = 10.0f;

    private float startTime;
    private float distance;
    public int diceRoll = 0;


    //use the 'moving' variable to check if the object has stopped
    [HideInInspector]
    public bool moving = true;

    [Header("Debug")]
    [Tooltip("You don't have to change that...")]
    public int currentWP = 0;
    [Tooltip("You don't have to change that...")]
    public int lastWP = 0;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // Check if a dice roll has occurred
        if (diceRoll > 0)
        {
            if (isInJail && diceRoll != 6) // Check if the player is in jail and didn't roll a 6
            {
                // Player cannot move if in jail and didn't roll a 6
                diceRoll = 0; // Reset the dice roll
            }
            else if (isInJail && diceRoll == 6) // Check if the player is in jail and rolled a 6
            {
                // Player rolled a 6 while in jail, so they can leave jail
                isInJail = false;
                currentWP = 10; // Move them to the corresponding waypoint
                diceRoll = 0; // Reset the dice roll
            }
            else if (!isInJail)
            {
                // Move the pawn to the waypoint that corresponds to the dice roll
                int targetWP = currentWP + diceRoll;

                // Reset the dice roll
                diceRoll = 0;

                // Start the MoveToWaypoint coroutine
                StartCoroutine(MoveToWaypoint(targetWP));
            }
        }
    }

    IEnumerator MoveToWaypoint(int targetWP)
    {
        // While the current waypoint is not the target waypoint
        while (currentWP != targetWP)
        {
            
            // Calculate the next waypoint
            int nextWP = currentWP + 1;

            if(nextWP > waypoints.Length - 1)
            {
                nextWP = 0;
                targetWP = targetWP - waypoints.Length;
            }

            // While the pawn is not at the next waypoint
            while (Vector3.Distance(transform.position, waypoints[nextWP].transform.position) > 0.001f)
            {
                // Move towards the next waypoint
                transform.position = Vector3.MoveTowards(transform.position, waypoints[nextWP].transform.position, speed * Time.deltaTime);
                yield return null;
            }

            // Update the current waypoint
            currentWP = nextWP;

            // Check if the pawn has landed on the "Go to Jail" waypoint
            if (targetWP == 30)  // Replace 30 with the index of your "Go to Jail" waypoint
            {
                // Move the pawn to the "Jail" waypoint
                transform.position = jailWaypoint.transform.position;

                // Set currentWP to 10 to fix the pathing.
                currentWP = 10;

                // Set the isInJail flag to true
                isInJail = true;

                // Stop moving
                moving = false;
                yield break;
            }
        }
    }
}
