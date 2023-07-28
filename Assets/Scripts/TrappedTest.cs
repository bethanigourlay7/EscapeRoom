using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using System;
/**
 * Script to test average magnitude vs velocit when robot is or is not trapped
 * I use dictionarys to display the average magnitude of velocity of the robot in each frame per second. 
 * */
public class TrappedTest : MonoBehaviour
{

    // see method SkipTrap()
    public bool skipTrapping5Secs = false;
    // see method createFileTestingRobotSpeed()
    public bool testRobotSpeed = false; 
    private string csvFile = "robotSpeedData.csv";
    Boolean fileCreated;

    // tracking seconds independently of frames 
    int seconds;
    int numOfFrames;

    // velocity of magnitude variables to calculate movement of robot 
    double vMagTotal;
    double avgVMag;


    // once robot is trapped, check if it has been trapped for 3 seconds
    int trappedCount;
    // need to make sure simultaneous seconds have passed since robot is trapped 
    int trappedOn;


    Dictionary<int, double> robotSpeedData; 


    RandomMovement rM;

    public NavMeshAgent robot;

    // Start is called before the first frame update
    void Start()
    {
        // assign robot
        robot = GetComponent<NavMeshAgent>();

        seconds = 0;
        numOfFrames = 0;
        vMagTotal = 0;

        trappedCount = 0;
        trappedOn = 0;
  
        // to display the average velocity per second in a table format at the end of each second
        robotSpeedData = new Dictionary<int, double>(); 

    }

    // Update is called once per frame
    void Update()
    {

        // tracking magnitude of velocity average
        vMagTotal = vMagTotal + robot.velocity.magnitude;
        numOfFrames++;



        // rounds seconds down to nearest whole number
        if ((seconds + 1) == (int)Time.unscaledTimeAsDouble)
        {
            seconds = seconds + 1;
            //Debug.Log("Another second has passed: " + seconds + " seconds");
            avgVMag = vMagTotal / numOfFrames;
            /*Debug.Log("Average magnitude of velocity on second" + seconds + " :      " + avgVMag);
            Debug.Log("Highest average magnitude of velocity " + maxVMagAvg);
            Debug.Log("Lowest average magnitude of velocity " + minVmagAvg);*/

            vMagTotal = 0;
            numOfFrames = 0;

            
            // add new line of speed data to dictionary each second
            robotSpeedData.Add(seconds, avgVMag);

            Debug.Log("Second\t| VMagAvg     ");


      

            
            if (avgVMag == 0 )
            {
                if(trappedOn == 0)
                {
                    trappedOn = seconds;
                    //Debug.Log("initial trap");
                    //Debug.Log("trapped on " + trappedOn);
                    trappedCount++;

                } else
                {
                    if(seconds == (trappedOn + trappedCount))
                        {
                        Debug.Log("Seconds: " + seconds);
                        Debug.Log("Trapped count:" + trappedCount);
                            trappedCount++;
                                // set so robot must be trapped for three seconds to trigger next stage 
                                if(trappedCount > 3 )
                                Debug.Log("robot is trapped");
                                robot.isStopped = true ;
                    }
                   /* else
                    {
                        trappedOn = 0;
                        trappedCount = 0;
                    }*/
                }
                
              
            }
            
            
           // DisplaySpeedData();

        }

        // for testing purposes
        skipTrap();
        createFileTestingRobotSpeed();



    }
    /**
     * Add first line to csv file for robot speed data
     * Appears in desktop and must be manually deleted after
     * each use
     */
    void CreateCSVFile()
    {
        // Add header row to the CSV file
        string headerRow = "Second,VMagAvg\n";
        AppendLogToFile(headerRow);
        fileCreated = true;
    }
    /**
     * Take in dictionary of speed data that includes 
     * the second and average velocity of magnitude of
     * the robots movement for each second.
     * Concatenates each line of data into a string to append to log file. 
     */
    void LogData(Dictionary<int, double> robotSpeedData)
    {
     
         foreach (KeyValuePair<int, double> robotSpeed in robotSpeedData)
            {

                 string robotSpeedDataSecond = robotSpeed.Key + "," + robotSpeed.Value + "\n";
                AppendLogToFile(robotSpeedDataSecond);

            }
        
    }

    // append to file code (chatGPT)
    void AppendLogToFile(string logMessage)
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), csvFile);
        File.AppendAllText(filePath, logMessage);
    }

    /**
     * Displays robot speed data to console 
     */
    void DisplaySpeedData()
    {
        // Concatenate all log messages into single string
        string logMessage = "Second\t| VMagAvg";
        foreach (KeyValuePair<int, double> robotSpeed in robotSpeedData)
        {
            logMessage += "\n" + robotSpeed.Key + "\t\t" + robotSpeed.Value;
        }


        Debug.Log(logMessage);
    }
    

    /**
     * If Test Robot speed toggled true in inspector,
     * create test file
     * */
    public void createFileTestingRobotSpeed()
    {
        if (testRobotSpeed == true && fileCreated == false)
        {
            if (seconds > 20)
            {
                CreateCSVFile();
                LogData(robotSpeedData);
                Debug.Log("File created");
            }
        }
    }

    /**
     * If autoTrapVariable toggled,
     * automatically trap robot after 5 seconds
     * */
    public void skipTrap()
    {
        if(!robot.isStopped && seconds > 5 && skipTrapping5Secs == true)
        {
            Debug.Log("Robot has automatically been trapped");
            robot.isStopped = true;
        }
    }
    

}
