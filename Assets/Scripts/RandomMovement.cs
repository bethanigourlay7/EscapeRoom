
using UnityEngine;
using UnityEngine.AI;


public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent robot;
    public float range; // radius of movement

    PickUpCollision pickUpCollision;

    //tracking distance travelled, i am calculating this in case I want to use it in the 
    // i set the distance travelled as one so that robot does not think it is trapped before moving
    private float totalDistanceTraveled = 1f;
  
    private Vector3 lastPosition;

    // for testing purposes
    float minDistance = 10000;

    public bool trappedThisFrame;
  

    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        Debug.Log("robot is "+ robot.isStopped);
        lastPosition = transform.position;

       
        
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

            if (pickUpCollision.collidedWithPickup)
            {
                Update();
            }
            //Debug.Log("Robot is moving to destination: " + randomPoint);
        }

       // check if robot is trapped 
        // Check if the robot is trapped in an obstacle (original chatGPT code )
        /*if (robot.velocity.magnitude <= 0.01f && robot.remainingDistance > robot.stoppingDistance && robot.remainingDistance < 10)
        {
                Debug.log("Robot is trapped);
        }*/

     
  

     
     
        lastPosition = transform.position;

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
