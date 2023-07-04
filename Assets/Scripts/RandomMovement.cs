using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

// ref - https://github.com/JonDevTutorial/RandomNavMeshMovement/blob/main/RandomMovement.cs
public class RandomMovement : MonoBehaviour 
{
    public NavMeshAgent robot;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of board

    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (robot.remainingDistance <= robot.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                robot.SetDestination(point);
                Debug.Log("Robot is moving to destination: " + point);
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }


}