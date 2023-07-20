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
    double minVmagAvg;
    double maxVMagAvg;

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
        minVmagAvg = 0;
        maxVMagAvg = 0;
  
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

            

            robotSpeedData.Add(seconds, avgVMag);

            Debug.Log("Second\t| VMagAvg     ");
            // chatGPT used here for C# foreach syntax
            foreach (KeyValuePair<int, double> robotSpeed in robotSpeedData)
            {

                Debug.Log(robotSpeed.Key + "\t\t" + robotSpeed.Value);

            }


            DisplaySpeedData();
            if (avgVMag == 0)
            {
                Debug.Log("robot is trapped");
                robot.isStopped = true ;
            }

        }

        if (seconds > 20 && fileCreated == false)
        {
            CreateCSVFile();
            LogData(robotSpeedData);
            Debug.Log("File created");
        }
        // getting min and max values too see how highest and lowest velocities in any given second
        if(avgVMag < minVmagAvg )
        {
            minVmagAvg = avgVMag;
            Debug.Log("New lowest average magnitude of velocity" );
        }
        if(avgVMag > maxVMagAvg)
        {
            maxVMagAvg = avgVMag;
            Debug.Log("New highest average magnitude of velocity ");

        }

    }

    void CreateCSVFile()
    {
        // Add header row to the CSV file
        string headerRow = "Second,VMagAvg\n";
        AppendLogToFile(headerRow);
        fileCreated = true;
    }

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
