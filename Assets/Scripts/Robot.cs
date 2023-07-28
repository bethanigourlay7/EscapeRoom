using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.IO;
using System;
public class Robot : MonoBehaviour
{
    public NavMeshAgent robot;
    public float range;
    private Animator animator;
    private Dictionary<int, double> robotSpeedData;
    public bool skipTrapping5Secs = false;
    public bool testRobotSpeed = false;
    private string csvFile = "robotSpeedData.csv";
    private bool fileCreated;

    [SerializeField] private GameObject environment;

    private int seconds;
    private int numOfFrames;
    private double vMagTotal;
    private double avgVMag;
    private int trappedCount;
    private int trappedOn;

    public bool isTrapped;


    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        robotSpeedData = new Dictionary<int, double>();
    }

    void Update()
    {
        CheckForRandomMovement();
        if (testRobotSpeed)
        {
            SpeedTest();
        }
        if (skipTrapping5Secs)
        {
            SkipTrap();
        }
      
    }

    Vector3 GetRandomPointInRange()
    {
        animator.SetBool("Walk_Anim", true);
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * range;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas);
        return hit.position;
    }

    void CheckForRandomMovement()
    {
        if (!robot.isStopped && !robot.pathPending && robot.remainingDistance <= robot.stoppingDistance)
        {
            Vector3 randomPoint = GetRandomPointInRange();
            robot.SetDestination(randomPoint);
        }
    }

    void SpeedTest()
    {
        vMagTotal += robot.velocity.magnitude;
        numOfFrames++;

        if ((seconds + 1) == (int)Time.unscaledTimeAsDouble)
        {
            seconds++;
            avgVMag = vMagTotal / numOfFrames;
            vMagTotal = 0;
            numOfFrames = 0;

            robotSpeedData.Add(seconds, avgVMag);

            if (avgVMag == 0)
            {
                if (trappedOn == 0)
                {
                    trappedOn = seconds;
                    trappedCount++;
                }
                else if (seconds == (trappedOn + trappedCount))
                {
                    trappedCount++;
                    if (trappedCount > 3)
                    {
                        robot.isStopped = true;
                        isTrapped = true;
                    }
                }
            }

            if (seconds > 20 && !fileCreated)
            {
                CreateCSVFile();
                LogData(robotSpeedData);
                fileCreated = true;
            }
        }
    }

    void CreateCSVFile()
    {
        string headerRow = "Second,VMagAvg\n";
        AppendLogToFile(headerRow);
    }

    void LogData(Dictionary<int, double> data)
    {
        foreach (KeyValuePair<int, double> robotSpeed in data)
        {
            string robotSpeedDataSecond = robotSpeed.Key + "," + robotSpeed.Value + "\n";
            AppendLogToFile(robotSpeedDataSecond);
        }
    }

    void AppendLogToFile(string logMessage)
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), csvFile);
        File.AppendAllText(filePath, logMessage);
    }

    void SkipTrap()
    {
        if (!robot.isStopped && seconds > 5)
        {
            robot.isStopped = true;
        }
    }

}