using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;
public class Robot : MonoBehaviour
{
    public NavMeshAgent robot;
    public float range;
    private Animator animator;
    private Dictionary<int, double> robotSpeedData;
    public bool skipTrapping10Secs = false;
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

    public bool isTrapped = false;

   

    public string beepBoop = "boop beep beep";

    // diagnostic variables, for use in the the Interpreter script when robot is being fixed
    public String robotModel { get;  set; }
    public String softwareVersion { get; set; }
    public String capacitorRatingFull { get;  set; }
    public String capacitorRating { get; set; }
    public String infraredSensitivity { get;  set; }

    // boolean to check if robot has been fixed
    public bool robotFixed = false;


    public bool terminalScene = false;

    // to check which scene is currently active
    string currentSceneName;


    void Start()
    {
        robot = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        robotSpeedData = new Dictionary<int, double>();
        isTrapped = false;

        // set robot diagnostic variables 
        robotModel = "123456";
        softwareVersion = "3.1.2";
        capacitorRatingFull = "3000";
        infraredSensitivity = "400";

        currentSceneName = SceneManager.GetActiveScene().name;


    }

    void Update()
    {

        if (currentSceneName != "TerminalCheck")
        {
            if (GameManager.InStageThree() == true)
            {
                Freestyle();
            }
            else if (terminalScene)
            {
                return;
            }
            else
            {
                Freestyle();
                //CheckForRandomMovement();
            }
            
        }else
        {
            return;
        }
    
       


        //Freestyle();

        if (testRobotSpeed)
        {
            SpeedTest();
        }
        if (skipTrapping10Secs == true)
        {
            Trap();
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
        Debug.Log("Speed test");
        vMagTotal += robot.velocity.magnitude;
        numOfFrames++;

        if ((seconds + 1) == (int)Time.unscaledTimeAsDouble)
        {
            seconds++;
            avgVMag = vMagTotal / numOfFrames;
            vMagTotal = 0;
            numOfFrames = 0;
            Debug.Log("seconds " + seconds);
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
                Debug.Log("creating file");
                CreateCSVFile();
                LogData(robotSpeedData);
                fileCreated = true;
            }
        }
    }
    /*
     * Freestyle mode
     */
    public void Freestyle()
    {
        if (TiltFive.Input.TryGetStickTilt(out var joyStickValue))
        {
            animator.SetBool("Walk_Anim", true);
            robot.transform.Translate(new Vector3(joyStickValue.x, 0, joyStickValue.y) * 4 * Time.deltaTime);
            Debug.Log("moving");
            if (robot.velocity.magnitude*100 == 0)
            {
                
                animator.SetBool("Walk_Anim", false);
                Debug.Log("not moving. vmag is " + robot.velocity*100);
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

    void Trap()
    {
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
                       // isTrapped = true;
                        robot.isStopped = true;
                        
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

        if (robot.isStopped == false && seconds > 10 && skipTrapping10Secs == true)
        {
            robot.isStopped = true;
            isTrapped = true;
            animator.SetBool("Walk_Anim", false);

        }
    }



}