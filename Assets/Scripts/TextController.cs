using System.Collections;
using UnityEngine;
using TMPro; // Import TextMesh Pro namespace

public class TextController : MonoBehaviour
{
    public TMP_Text uiText; // Use TMP_Text instead of Text
    public float delay = 0.1f;
    private string myString;
    public GameObject UITextObject;

   // private bool textOngoing;

    // stages and text

    //tutorial string
    public string tutorialString = "Hold the wand in your hand and move it around. You should see a pink trail at the tip of the wand, this means it is connected. Once you can see the pink trail, you can interact with objects in the scene. Point the wand at the cube and press the trigger to pick it up. Once you are comfortable with the feel of the wand, press Exit Tutorial to start. ";

    // start of game
    public string startText = "Hi user, your smart home robot appears to be malfunctioning and is not responding to commands. Find the RED manual to see instructions on how to fix the robot. . Press the trigger on the help button to open and close this window.";

    // prompt user to find manual
    public string findManualText = "There is a RED manual that may have instructions on how to fix your robot. Press the trigger on the help button to open and close this window.";

    // robot has been trapped 

    //public string robotTrapped = "Well done, now the robot has been trapped, see the manual for next steps. Hit help to open and close this window.";

    public string remoteFounf = "Well done, now you have found the remote control, now you need to find the RED manual. Press the trigger on the help button to open and close this window.";

    public string manualFound = "Well done, you have found the manual";

    public string freeStyleString;

    // Game manager to reference what stage the game is in
    GameManager gameManager;

    

    void Start()
    {
       // myString = uiText.text; // Get the current text in the TMP_Text component
        uiText.text = ""; // Clear the text
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.Log("Game mana not exist");
        }

        manualFound = "Well done, you have found the manual, if you have not found the remote control it should be around the sitting area.";
        freeStyleString = "Woohoo, now the robot has been fixed, now you are in freestyle mode. Use the joystick on the controller to move the robot";
        remoteFounf = "Well done, now you have found the remote control, now you need to find the RED manual. Press the trigger on the help button to open and close this window.";
    }

    public IEnumerator DisplayTextOverTime(string currentString)
    {
        uiText.text = "";
        string text = currentString;
        if(UITextObject.activeInHierarchy == false)
        {
            UITextObject.SetActive(true);
        }
        
        Debug.Log("The text in the text variable is " +text);
       
        for (int i = 0; i < text.Length; i++)
        {
            uiText.text += text[i] ;
            yield return new WaitForSeconds(delay);
        }
     
    }
        
    public string CurrentString()
    {
        string returnString;
        returnString = "";
        if (gameManager.InTutorial())
        {
            returnString = tutorialString;
        }
        else if (gameManager.InStageOne())
        {
            if (gameManager.robotAgent.robot.isStopped == false)
            {
                returnString = startText;
            }
        }
        else if (gameManager.InStageTwo())
        {
            Debug.Log( "in stage 2");
            returnString = remoteFounf;
        }else if( gameManager.InStageFour())
        {
            Debug.Log("in stage 3");
            returnString = freeStyleString;
        }
        else
        {
            returnString =  "i dont know where i am";
        }
        return returnString;
    }

    /**
     * Checks if text is ongoing 
     */
    bool IsCoroutineRunning(Coroutine coroutine)
    {
        return coroutine != null;
    }
}
