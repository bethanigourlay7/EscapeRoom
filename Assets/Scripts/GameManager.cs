using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject inputManagerObject;

    // this contains the terminal object and by extension all associated scripts
    private InputManager inputManager;

    public GameObject terminalManager;
    /*
     * Button to open and close the terminal window, only available from stage 2 onwards
     */
   // public GameObject terminalButton;
    /*
     * Button to open and close the manual, available throughout the whole game
     */
    public GameObject manualButton;

    /*
     * Activated after both the remote and manual have been found
     */
    public GameObject SceneChangeButton;
    /*
     * !!!!!!!!!!!!!!!!!!!!!!!!!
     */
    public GameObject environment;
    /*
     * object to access robot components
     */
    public GameObject robotObject;
    /*
     * Access scripting variables
     */
    public Robot robotAgent;

    public GameObject wandPointerView;
    /*
     * 
     * !!!!!!
     */
    public GameObject book;

    public GameObject UITextObject;
    public GameObject UITextDisplay;
   

    // text controller to display relevant information to user

    private TextController textController;

    // object manager script to manipulate settings in game objects 

    private ObjectManager objectManager;

    // variables to show what stage the game is in
    public bool atTutorial = true;
    public bool atStageOne = false;
    public bool atStageTwo = false;
    public bool atStageThree = false; 
    public bool atStageFour = false;

    public bool remoteControlFound = false;
    public bool manualFound = false;
   

    // Start is called before the first frame update
    private void Start()

    {      
        //setting everything to false at start of game
        terminalManager.SetActive(false);
       
        UITextDisplay.SetActive(true);
        textController = UITextDisplay.GetComponent<TextController>();
        objectManager = GetComponent<ObjectManager>();
        //UIText.SetActive(false);
        //stageOneInput = inputManager.GetComponent<StageOneInput>();
        
        robotObject.SetActive(false);

        if (robotAgent != null)
        {
            Debug.Log("robot exists");
        }
        else
        {
            Debug.Log("Robot does not exist");
        }
        // tutorial text
<<<<<<< HEAD
        /*atTutorial = true;
=======
       /* atTutorial = true;
>>>>>>> eb88d88fb7d5cbef70efe555234e99f622aa7cbd
        if (atTutorial == true)
        {
           // terminalManager.SetActive(false);
            Debug.Log("at tutorial");
          
            StartCoroutine(textController.DisplayTextOverTime(textController.tutorialString));
        }*/
    }

    // Update is called once per frame
    private void Update()
    {

        
        /*
        if(remoteControlFound != true && terminalManager.activeInHierarchy == true)
        {
            terminalManager.SetActive(false);
        }
        */
        if(terminalManager.activeInHierarchy == true)
        {
            wandPointerView.SetActive(false);
        }
        Debug.Log("In update at tutorial is " + atTutorial);
       // stage 1 will start as soon as start is pressed in the menu and
        if (atStageOne == false && atStageTwo == false && atStageFour == false && atTutorial == false)
        {
            atStageOne = true;
            StageOne();
            if(robotAgent != null)
            {
               Debug.Log( "Robot agent active at stage 1");
            }
        }

        //Debug.Log("robot stoppped " + robotAgent.robot.isStopped);

        // after the robot has been trapped, move on to stage 2
        if (robotAgent != null)
        {
             if (/*robotAgent.isTrapped*/ remoteControlFound == true && atStageTwo == false && atStageOne == true) 
                    {
                        Debug.Log("Stage 2 has started ");

                        atStageTwo = true;
                        atStageOne = false;
           
                          // StageTwo();
            
                    }
           /* if (robotAgent.robotFixed == true)
            {
               // Debug.Log("robot is fixed");
            }*/
        }
        if(remoteControlFound && manualFound && atStageThree == false/*&&SceneChangeButton.activeInHierarchy == false*/)
        {
            atStageThree = true;
            StopAllCoroutines();
            StartCoroutine(textController.DisplayTextOverTime("Now you can access the robots terminal, click the manual button to find instructions on how to fix the terminal"));
            //SceneChangeButton.SetActive(true);
        }
        // after the robot is fixed in the terminal, manually trigger Stage Three where the robot can be controlled
        if(atStageFour == true && robotAgent.robotFixed == false)
        {
            robotAgent.robotFixed = true;
            Debug.Log("Robot is fixed = " + robotAgent.robotFixed);
            atStageFour =true;
            DisplayText();
        }

        /*if(atStageTwo == true && robotAgent.robotFixed == true)
        {
            atStageThree = true;
        }*/
    }

    /*
     * Starts the tutorial UIText
     */
    public void Tutorial()
    {
        atTutorial = true;
        objectManager.UnfreezeBookCase();
        if (atTutorial == true)
        {
            UITextObject.SetActive(true);
            // terminalManager.SetActive(false);
            Debug.Log("at tutorial");
            StartCoroutine(textController.DisplayTextOverTime(textController.tutorialString));
        }
    }


    public void Tutorial()
    {
        atTutorial = true;
        if (atTutorial == true)
        {
           
            // terminalManager.SetActive(false);
            Debug.Log("at tutorial");

            StartCoroutine(textController.DisplayTextOverTime(textController.tutorialString));
        }
    }

    /**
     * Start game
     */
    private void StageOne()
    {
          robotObject.SetActive(true);
        // activate robot agent reference once tutorial is complete
        robotAgent = robotObject.GetComponent<Robot>();
        robotAgent = GameObject.FindObjectOfType<Robot>();

        Debug.Log("Stage1 started");

        // start displaying text from the text controller

        DisplayText();

        // level 1
        inputManagerObject.SetActive(true);
        robotObject.SetActive(true);
        environment.SetActive(true);
        // ensure terminal button is not available
      //  terminalButton.SetActive(false);
    }

    /*
     * Occurs once remote and manual have been found
     */
    public void ShiftToManual()
    {
        UITextObject.SetActive(false);
        robotObject.SetActive(false);
        book.SetActive(true);
    }

    public void freeStyleMode()
    {
        
    }

    /**
     * Occurs after robot is trapped
     */
  /*  private void StageTwo()
    {
        // add a text here indicating next stage of game

        DisplayText();
       //     terminalManager.SetActive(true);
             terminalButton.SetActive(true);
        *//*environment.SetActive(false);
        robotObject.SetActive(false);
        UITextObject.SetActive(false);*//*
    }*/

    private void StageFour()
    {
        DisplayText();
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
        
        StartCoroutine(textController.DisplayTextOverTime(textController.CurrentString()));
        Debug.Log("current string" + textController.CurrentString());
    }
    


    public bool InTutorial()
    {
        if (atTutorial == true)
        {
            return true;
        }
        else
        {
            return false;
        }
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
    
    public void StageTwoScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    /**
    Getter for at stage three, used in text controller for current string
 * */
    public bool InStageFour()
    {
        if (atStageFour == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}