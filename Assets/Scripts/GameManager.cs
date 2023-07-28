using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject inputManagerObject;
    // this contains the terminal object and by extension all associated scripts
    private InputManager inputManager;
    public GameObject terminalManager;

    public GameObject environment;
    public GameObject robotObject;
    private Robot robot;
    public GameObject book;

    bool stage1Started;
    bool stage2Started;
    bool stage3Started;

    // Start is called before the first frame update
    void Start()
    {

        StageOne();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (robot.isTrapped && stage2Started == false)
        {
            stage2Started = true;
            StageTwo();
            
        }


    }

    /**
     * 
     * Start game 
     */
    void StageOne()
    {
        robot = robotObject.GetComponent<Robot>();

        // level 1
        inputManagerObject.SetActive(true);
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
        robotObject.SetActive(false);
        terminalManager.SetActive(true);
        environment.SetActive(false);
    }
}
