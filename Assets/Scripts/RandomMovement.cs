
using UnityEngine;
using UnityEngine.AI;


public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent robot;
    public float range; // radius of movement

    PickUpCollision pickUpCollision;

 
  
    private Vector3 lastPosition;


    public bool trappedThisFrame;
  

    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        Debug.Log("robot is "+ robot.isStopped);
       
        
    }

    void Update()
    {

        if (!robot.isStopped)
        {
//Debug.Log("remaining distance" + robot.remainingDistance);
        if (!robot.pathPending && robot.remainingDistance <= robot.stoppingDistance && robot.isStopped == false)
        {
            Vector3 randomPoint = GetRandomPointInRange();
            robot.SetDestination(randomPoint);

            
        }

       // check if robot is trapped 
        // Check if the robot is trapped in an obstacle (original chatGPT code )
        /**/

      

        }

    }

    Vector3 GetRandomPointInRange()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas);
        return hit.position;
    }


}
