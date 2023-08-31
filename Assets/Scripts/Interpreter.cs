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

    // variables used in easy mode
    int count = 0;
    bool started;

    // variables to check robot spec against 
    string latestSoftwareVersion = "3.1.3";
    string energyTarget = "6";
    string eyesightTarget = "8";


    public List<string> Interpret(string userInput)
    {
        response.Clear();
        // help --command

        string[] args = userInput.Split();

        if (args[0] == "help")
        {
            // return reponse
            response.Add("robotdetails - see all about me ");
            response.Add("seteyesight [1-10] - if you type in seteyesight [insert number] you will make my eyesight sharper or blurrier");
            response.Add("setenergy [1-10] - if you type in setenergy [insert number] you will make me use more or less power");
            response.Add("amiupdated - see if I'm updated");
            response.Add("update - update robot to most recent version");
            response.Add("sleep - robot takes a nap");
            return response;
        }
        else if (args[0] == "robotdetails")
        {
            response.Add("Robot spec");
            response.Add("Model : " + robot.robotModel);
            response.Add("Current software version : " + robot.softwareVersion);
            response.Add("Robots Maximum Energy Level : " + robot.maxEnergyLevel);
            response.Add("Robots current energy level : " + robot.energyLevel);
            response.Add("Robots eyesight : " + robot.eyesight);

            return response;
        }
        else if (args[0] == "seteyesight")
        {
            Debug.Log(args[1]);
            robot.eyesight = args[1];
            response.Add("robots eyesight set to " + robot.eyesight);
            return response;

        }
        else if (args[0] == "setenergy")
        {
            robot.energyLevel = args[1];
            response.Add("robot energy level set to " + robot.energyLevel);
            return response;
        }
        else if (args[0] == "amiupdated")
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
        else if (args[0] == "update")
        {
            /*
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
            */
            robot.softwareVersion ="3.1.3";
            response.Add("Robot updated to most recent version");
            
            return response;
        }
        else if (args[0] == "reboot")
        {
            response.Add("Robot systems reset");
            response.Add("Model : " + robot.robotModel);
            response.Add("Current software version : " + robot.softwareVersion);
            response.Add("Robots Max Capacitor rating : " + robot.maxEnergyLevel);
            response.Add("Robots current capacitor rating : " + robot.energyLevel);
            response.Add("Robots infrared sensitivity : " + robot.eyesight);

            response.Add(robotFixCheckHard());

            return response;

        }
        else
        {
            response.Add("Command not recognised. Type help for a list of commands.");

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
            return response;
        }
        
        if (count == 0 && args[0] == "go" && started == true) 
        {
            
            response.Add("1 " + beepBoopArgs[0]);
            count += 1;
            return response;
        } else if (count == 1 && args[0] == "boop")
        {
            response.Add("2 " +beepBoopArgs[1]);
            count += 1;
            return response;
        } else if (count == 2 && args[0] == "boop" && args[1] == "beep")  {
            response.Add("3" + beepBoopArgs[2]);
            count += 1;
            return response;
        } else if (count == 3 && args[0] == "boop" && args[1] == "beep" && args[2] == "beep")
        {
            response.Add("You've won!");
            return response;
        } 
        else if(started == true )
        {
            started = false;
            response.Add("If you want to use the terminal type, 'boop'");
            return response;

        }
        else if(started == false)
        {
            response.Add("If you want to use the terminal type, 'boop'");
            return response;
        }
       
        return response;
    }

    string robotFixCheckHard()
    {

        string response;
        if(robot.softwareVersion == latestSoftwareVersion && robot.energyLevel == energyTarget && robot.eyesight == eyesightTarget)
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
