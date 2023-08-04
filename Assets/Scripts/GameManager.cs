using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject inputManagerObject;
    // this contains the terminal object and by extension all associated scripts
    private InputManager inputManager;
    private StageOneInput stageOneInput;
    public GameObject terminalManager;

    public GameObject environment;
    public GameObject robotObject;
    Robot robotAgent;
    public GameObject book;

    public GameObject UITextObject;
    public GameObject UITextDisplay;

    // text controller to display relevant information to user

    TextController textController;

    bool stage1Started = false;
    bool stage2Started = false;
    
    bool stage3Started;

    // Start is called before the first frame update
    void Start()
    {


        textController = UITextDisplay.GetComponent<TextController>(); 
        //UIText.SetActive(false);
        //stageOneInput = inputManager.GetComponent<StageOneInput>();
        robotAgent = GameObject.FindObjectOfType<Robot>();

      
        if(robotAgent!= null)
        {
            Debug.Log("robot exists");

        }
        else
        {
            Debug.Log("Robot does not exist");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // stage 1 will start as soon as start is pressed in the menu and 
        if(stage1Started == false)
        {
          
            stage1Started = true;
            StageOne();
            
        }

        //Debug.Log("robot stoppped " + robotAgent.robot.isStopped);

        // after the robot has been trapped, move on to stage 2
        if (robotAgent.robot.isStopped == true && stage2Started == false)
        {
            Debug.Log("Stage 2 started " + stage2Started);
            stage2Started = true;
            StageTwo();
        }
    }

    /**
     * Start game 
     */
    void StageOne()
    {

        Debug.Log("Stage1 started");
 
        // start displaying text from the text controller 
        StartCoroutine( textController.DisplayTextOverTime(textController.startText));

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
    void StageTwo()
    {

        // add a text here indicating next stage of game 
        Debug.Log("Stage 2 has started ");
        terminalManager.SetActive(true);
      
        environment.SetActive(false);
        robotObject.SetActive(false);

    }
}
