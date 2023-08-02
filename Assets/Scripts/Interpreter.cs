using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Script where I create all commands and interactions with the terminal 
 */
public class Interpreter : MonoBehaviour
{

    List<string> response = new List<string>(); 



    public List<string> Interpret(string userInput)
    {
        response.Clear();
        // help --command

        string[] args = userInput.Split();

        if(args[0] == "--help")
        {
            // return reponse
            response.Add("If you want to use the terminal type, 'boop'");
            response.Add(" --run_diagnostics - view ");
            response.Add(" --calibrate sensor infrared [insert num] - calibrates ");
            response.Add(" --set capacitor [insert num]");
           
            response.Add("Second response line");

            return response;
        }
    
        else
        {
            response.Add("Command not recognised. Type --help for a list of commands.");

            return response; 
        }
    }



}
