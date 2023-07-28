using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTerminal : MonoBehaviour
{
    Robot robot;
    [SerializeField] private GameObject terminal;
    [SerializeField] private GameObject environment;

    bool robotMalfunctioning;
    

    // Start is called before the first frame update
    void Start()
    {
        terminal.SetActive(false);
        robot = GameObject.FindObjectOfType<Robot>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTerminal();
    }

    /**
     * Displays terminal and hides environment within scene
     */
    void DisplayTerminal()
    {
        if (robot != null && robot.robot.isStopped) // checking if the Robot is stopped
        {
            terminal.SetActive(true);
            environment.SetActive(false);
            Debug.Log("Robot is trapped. Ready to display terminal.");
        }
    }

}
