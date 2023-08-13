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
    string latestSoftwareVersion = "1.1.3";
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
            response.Add("--softwareupdate [insert num]- update robot to most recent version");
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
            if(args[1].Length > 0)
            {
                if(args[1] == latestSoftwareVersion )
                {
                    robot.softwareVersion = args[1];
                    response.Add("Robot software version set to 1.1.3");
                } 
                else if(args[1] == "1.1.2")
                {
                    robot.softwareVersion = args[1];
                    response.Add("Robot software version set to 1.1.2");
                }  
                else if (args[1] == "1.1.1" )
                 {
                    robot.softwareVersion = args[1];
                    response.Add("Robot software version set to 1.1.1");

                 } else 
                    {
                        response.Add("This software version does not exist.");
                    }
            } else
            {
                response.Add("You did not specify a software type.");
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

            response.Add(robotFixCheckHard());

            return response;

        }
        else
        {
            response.Add("Command not recognised. Type --help for a list of commands.");

            return response;
        }
    }
    /*
     * Added an easy level interpreter option
     */
    public List<string> InterpretEasy(string userInput)
    {
        response.Clear();
        // help --command

        string[] args = userInput.Split();
        string[] beepBoopArgs = robot.beepBoop.Split();
        int beepBoopCount = beepBoopArgs.Length;
        int count = 0;
        bool started = false;

        string[] commands = { "beep", "boop", "go" };

        if(args[0] == "boop")
        {
            if(started == false)
            {
                 count = 0;
            }
           
            response.Add("Hi friend, lets play a game.");
            response.Add("When I say 'beep' you say 'boop' and remember the pattern" );
            response.Add("For example, I say...");
            response.Add("boop");
            response.Add("you say...");
            response.Add("beep");
            response.Add("then if i say beep, you remember your previous answer then say 'boop' like this...");
            response.Add("beep boop");
            response.Add("And so on, type 'go' to start :)");
            started = true; 
        }
        
        if (count == 0 && args[0] == "go" && started == true) 
        {
            response.Add("1 " + beepBoopArgs[0]);
            count += 1;
        } else if (count == 1 && args[0] == "beep")
        {
            response.Add("2 " +beepBoopArgs[1]);
            count += 1;
        } else if (count == 2 && args[0] == "beep" && args[1] == "boop")  {
            response.Add("3" + beepBoopArgs[2]);
            count += 1;
        } else if (count == 3 && args[0] == "beep" && args[1] == "boop" && args[2] == "boop")
        {
            response.Add("You've won!");
        } 
        else if(started == true )
        {
            
            started = false;
            response.Add("If you want to use the terminal type, 'boop'");

        }
       
        return response;
    }

    string robotFixCheckHard()
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
