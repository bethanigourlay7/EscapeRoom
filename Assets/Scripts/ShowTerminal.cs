using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTerminal : MonoBehaviour
{
    Robot robot;
    [SerializeField] private GameObject terminal;
    [SerializeField] private GameObject environment;
    private GameManager gameManager;

    bool robotMalfunctioning;
    bool terminalDisplayed;
  

    // Start is called before the first frame update
    void Start()
    {
        terminal.SetActive(false);
        robot = GameObject.FindObjectOfType<Robot>();
        terminalDisplayed = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(terminalDisplayed == false)
        {
             DisplayTerminal();
            terminalDisplayed = true;
        }
       
    }

    /**
     * Displays terminal and hides environment within scene
     */
    void DisplayTerminal()
    {
            terminal.SetActive(true);
            environment.SetActive(false);
            Debug.Log("Robot is trapped. Ready to display terminal.");
           
            terminalDisplayed = true;
 
    }

}
