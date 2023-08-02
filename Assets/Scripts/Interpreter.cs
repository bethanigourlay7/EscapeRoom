using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;



/**
 * Script where I create all commands and interactions with the terminal 
 */
public class Interpreter : MonoBehaviour
{

    List<string> response = new List<string>();

    public Robot robot;



    // variables to check robot spec against 
    string latestSoftwareVersion = "3.1.3";
    string capacitorTarget = "2400";
    string sensorTarget = "850";


    public List<string> Interpret(string userInput)
    {
        response.Clear();
        // help --command

        string[] args = userInput.Split();

        if (args[0] == "--help")
        {
            // return reponse
            response.Add("If you want to use the terminal type, 'boop'");
            response.Add("--robotspec - view all robot specifications ");
            response.Add("--calibratesensorinfrared [insert num] - calibrates the sensor to specified number ");
            response.Add("--setcapacitor [insert num] - sets capacitor to specified number");
            response.Add("--checkforupdates - checks if robots software version is up to date");
            response.Add("--softwareupdate - update robot to most recent version");
            response.Add("--reboot - reboots robot system");

 

            return response;
        }
        else if (args[0] == "--robotspec")
        {
            response.Add("Robot spec");
            response.Add("Model : " + robot.robotModel);
            response.Add("Current software version : " + robot.softwareVersion);
            response.Add("Robots Max Capacitor rating : " + robot.capacitorRatingFull);
            response.Add("Robots current capacitor rating : " + robot.capacitorRating);
            response.Add("Robots infrared sensitivity : " + robot.infraredSensitivity);

            return response;
        }
        else if (args[0] == "--calibratesensorinfrared")
        {
            Debug.Log(args[1]);
            robot.infraredSensitivity = args[1];
            response.Add("infrared sensitivity recalibrated to " + robot.infraredSensitivity);
            return response;

        }
        else if (args[0] == "--setcapacitor")
        {
            robot.capacitorRating = args[1];
            response.Add("Capacitor rating set to " + robot.capacitorRating);
            return response;
        }
        else if (args[0] == "--checkforupdates")
        {
            if (robot.softwareVersion == latestSoftwareVersion)
            {
                response.Add("Robot is up to date");
            }
            else if (robot.softwareVersion != latestSoftwareVersion)
            {
                response.Add("Software update available ");
            }
            return response;
        }
        else if (args[0] == "--softwareupdate")
        {

            robot.softwareVersion = args[1];
            if (robot.softwareVersion == latestSoftwareVersion)
            {
                response.Add("Robot is up to date");
            }
            else
            {
                response.Add("Robot is not up to date");
            }
            return response;
        }
        else if (args[0] == "--reboot")
        {
            response.Add("Robot systems reset");
            response.Add("Model : " + robot.robotModel);
            response.Add("Current software version : " + robot.softwareVersion);
            response.Add("Robots Max Capacitor rating : " + robot.capacitorRatingFull);
            response.Add("Robots current capacitor rating : " + robot.capacitorRating);
            response.Add("Robots infrared sensitivity : " + robot.infraredSensitivity);

            response.Add(robotFixCheck());

            return response;

        }
        else
        {
            response.Add("Command not recognised. Type --help for a list of commands.");

            return response;
        }
    }


    string robotFixCheck()
    {

        string response;
        if(robot.softwareVersion == latestSoftwareVersion && robot.capacitorRating == capacitorTarget && robot.infraredSensitivity == sensorTarget)
        {
            response = ("Sentience rectification protocol completed");
            robot.robotFixed = true; 
            return response;
        }
        else
        {
            response = "!! Robot malfunctioning !!";
        }
        return response;
    }


}
