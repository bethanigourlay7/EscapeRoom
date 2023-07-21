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

    private string csvFile = "robotSpeedData.csv";
    Boolean fileCreated;

    // tracking seconds independently of frames 
    int seconds;
  
    int numOfFrames;

    // mag
    double vMagTotal;
    double avgVMag;

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
            Debug.Log("Another second has passed: " + seconds + " seconds");
            avgVMag = vMagTotal / numOfFrames;
            /*Debug.Log("Average magnitude of velocity on second" + seconds + " :      " + avgVMag);
            Debug.Log("Highest average magnitude of velocity " + maxVMagAvg);
            Debug.Log("Lowest average magnitude of velocity " + minVmagAvg);*/

            vMagTotal = 0;
            numOfFrames = 0;

            
            // add new line of speed data to dictionary each second
            robotSpeedData.Add(seconds, avgVMag);

            Debug.Log("Second\t| VMagAvg     ");

           


            
            if (avgVMag == 0)
            {
                Debug.Log("robot is trapped");
                robot.isStopped = true ;
            }
            
            
           // DisplaySpeedData();

        }

        if (seconds > 20 && fileCreated == false)
        {
            CreateCSVFile();
            LogData(robotSpeedData);
            Debug.Log("File created");
        }
      

    }
    /**
     * Add first line to csv file for robot speed data
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

}
