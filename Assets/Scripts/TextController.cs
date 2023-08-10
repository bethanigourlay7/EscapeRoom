using System.Collections;
using UnityEngine;
using TMPro; // Import TextMesh Pro namespace

public class TextController : MonoBehaviour
{
    public TMP_Text uiText; // Use TMP_Text instead of Text
    public float delay = 0.1f;
    private string myString;

    private bool textOngoing;

    // stages and text
    // start of game
    public string startText = "Hi user, your smart home robot appears to be malfunctioning and is not responding to commands. Find the manual to see instructions on how to fix the robot. Hit help to open and close this windoww";

    // prompt user to find manual
    public string findManualText = "There is a manual that may have instructions on how to fix your robot. Hit help to open and close this window.";

    // robot has been trapped 

    public string robotTrapped = "Well done, now the robot has been trapped, see the manual to for next steps. Hit help to open and close this window.";

    // Game manager to reference what stage the game is in
    GameManager gameManager;

    

    void Start()
    {
        myString = uiText.text; // Get the current text in the TMP_Text component
        uiText.text = ""; // Clear the text
        gameManager = FindAnyObjectByType<GameManager>();
      
    }

    public IEnumerator DisplayTextOverTime()
    {
        uiText.text = "";
        string text = this.CurrentString();
        Debug.Log("The text in the ui is " + uiText.text);
        
        Debug.Log("The text in the text variable is " +text);
        textOngoing = true;
        for (int i = 0; i < text.Length; i++)
        {
            uiText.text += text[i] ;
            yield return new WaitForSeconds(delay);
        }
        textOngoing = false;
    }
        
    public string CurrentString()
    {
        string returnString;
        returnString = "";
        if (gameManager.InStageOne())
        {
            if (gameManager.robotAgent.robot.isStopped == false)
            {
                returnString = startText;
            }
           
        }
        else if (gameManager.InStageTwo())
        {
            Debug.Log( "in stage 2");
            returnString = robotTrapped;
        }else if(gameManager.InStageThree())
        {
            Debug.Log("in stage 3");
            returnString = "finished";
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
    public bool IsTextOngoing()
    {
        return textOngoing;
    }
}
