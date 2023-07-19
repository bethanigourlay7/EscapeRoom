
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent robot;
    public float range; // radius of movement



    //tracking distance travelled, i am calculating this in case I want to use it in the 
    // i set the distance travelled as one so that robot does not think it is trapped before moving
    private float totalDistanceTraveled = 1f;
    // make sure numOfMovements > 1 before checking if robot is trapped
    private int numOfMovements = 0;
    private Vector3 lastPosition;

    public PickUpCollision pickUpCollision;

    // for testing purposes
    float minDistance = 10000;
   
  
  

    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        Debug.Log("robot is "+ robot.isStopped);
        lastPosition = transform.position;
        
        
    }

    void Update()
    {

        //Debug.Log("remaining distance" + robot.remainingDistance);
        if (!robot.pathPending && robot.remainingDistance <= robot.stoppingDistance && robot.isStopped == false)
        {
            Vector3 randomPoint = GetRandomPointInRange();
            robot.SetDestination(randomPoint);

            if (pickUpCollision.collidedWithPickup)
            {
                Update();
            }
            //Debug.Log("Robot is moving to destination: " + randomPoint);
        }

      /*  // check if robot is trapped 
        // Check if the robot is trapped in an obstacle (chatGPT)
        if (robot.velocity.magnitude <= 0.01f && robot.remainingDistance > robot.stoppingDistance)
        {
            robot.isStopped = true;
            Debug.Log("Robot is trapped in an obstacle.");
        }*/

        // Calculate distance traveled
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition) * 10000;
        //Debug.Log("Distance travelled this frame " + distanceThisFrame);
  

        // checking min distance
        if (distanceThisFrame < minDistance)
        {
            minDistance = distanceThisFrame;
            
            Debug.Log("New min distance is " + minDistance);
        }
        totalDistanceTraveled += distanceThisFrame;
        //Debug.Log("Total distance travelled " + totalDistanceTraveled);
        lastPosition = transform.position;
        if(distanceThisFrame > 2)
        {
            numOfMovements++;
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
