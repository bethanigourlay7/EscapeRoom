using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent robot;
    public float range; // radius of movement

    //tracking distance travelled, i am calculating this in case I want to use it in the 
    // future 
    private float totalDistanceTraveled = 0f;
    private Vector3 lastPosition;


    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!robot.pathPending && robot.remainingDistance <= robot.stoppingDistance)
        {
            Vector3 randomPoint = GetRandomPointInRange();
            robot.SetDestination(randomPoint);
            //Debug.Log("Robot is moving to destination: " + randomPoint);
        }

         // Calculate distance traveled
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        //Debug.Log("Distance travelled this frame " + distanceThisFrame);
        totalDistanceTraveled += distanceThisFrame;
        //Debug.Log("Total distance travelled " + totalDistanceTraveled);
        lastPosition = transform.position;
    }

    Vector3 GetRandomPointInRange()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas);
        return hit.position;
    }
    // being used for when robot collides with object
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            float distanceThisTrigger = Vector3.Distance(other.transform.position, lastPosition);
           // totalDistanceTraveled += distanceThisTrigger;
            lastPosition = other.transform.position;
            Debug.Log("Robot entered trigger. Distance traveled: " + distanceThisTrigger);
        }
    }
}
