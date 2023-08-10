using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject inputManagerObject;

    // this contains the terminal object and by extension all associated scripts
    private InputManager inputManager;

    public GameObject terminalManager;

    public GameObject environment;
    public GameObject robotObject;
    public Robot robotAgent;
    public GameObject book;

    public GameObject UITextObject;
    public GameObject UITextDisplay;

    // text controller to display relevant information to user

    private TextController textController;

    // variables to show what stage the game is in
    private bool atStageOne = false;

    private bool atStageTwo = false;
    private bool atStageThree = false;

    // Start is called before the first frame update
    private void Start()
    {
        textController = UITextDisplay.GetComponent<TextController>();
        //UIText.SetActive(false);
        //stageOneInput = inputManager.GetComponent<StageOneInput>();
        robotAgent = GameObject.FindObjectOfType<Robot>();

        if (robotAgent != null)
        {
            Debug.Log("robot exists");
        }
        else
        {
            Debug.Log("Robot does not exist");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // stage 1 will start as soon as start is pressed in the menu and
        if (atStageOne == false && atStageTwo == false && atStageThree == false)
        {
            atStageOne = true;
            StageOne();
        }

        //Debug.Log("robot stoppped " + robotAgent.robot.isStopped);

        // after the robot has been trapped, move on to stage 2
        if (robotAgent.isTrapped == true && atStageTwo == false && atStageOne == true) 
        {
            Debug.Log("Stage 2 has started ");

            atStageTwo = true;
            atStageOne = false;
            DisplayText();
            if (textController.IsTextOngoing() == false)
            {
                StageTwo();
            }
        }
    }

    /**
     * Start game
     */

    private void StageOne()
    {
        Debug.Log("Stage1 started");

        // start displaying text from the text controller

        DisplayText();

        robotAgent = robotObject.GetComponent<Robot>();

        // level 1
        inputManagerObject.SetActive(true);
        // stageOneInput.enabled();
        robotObject.SetActive(true);
        terminalManager.SetActive(false);
        // add text here indicating robot is trapped and pass to next stage
    }

    /**
     * Occurs after robot is trapped
     */

    private void StageTwo()
    {
        // add a text here indicating next stage of game
      
       
            terminalManager.SetActive(true);
            environment.SetActive(false);
            robotObject.SetActive(false);
            UITextObject.SetActive(false);
        
        
    }

    /*
     * Uses a coroutine to display text indepenedent of frames, stops any previous coroutines from previous text displays
     *
     */

    private void DisplayText()
    {
        StopAllCoroutines();
        Debug.Log("In display text method " );

        UITextObject.SetActive(true);
        StartCoroutine(textController.DisplayTextOverTime());
    }

    /**
     * Getter for at stage one
     * */

    public bool InStageOne()
    {
        if (atStageOne == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
 *     Getter for at stage two
 * */

    public bool InStageTwo()
    {
        if (atStageTwo == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
 *     Getter for at stage three
 * */

    public bool InStageThree()
    {
        if (atStageThree == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}