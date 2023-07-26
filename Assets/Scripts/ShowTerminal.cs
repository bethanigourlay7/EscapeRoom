using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTerminal : MonoBehaviour
{
    TrappedTest trapped;
    [SerializeField] public GameObject terminal;
    [SerializeField] public GameObject environment;

    bool robotMalfunctioning;

    // Start is called before the first frame update
    void Start()
    {
        terminal.SetActive(false);
        trapped = GameObject.FindObjectOfType<TrappedTest>();
     
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
        if (trapped != null && trapped.robot.isStopped)
        {
            terminal.SetActive(true);
            environment.SetActive(false);
            Debug.Log("Robot is trapped. Ready to display terminal.");


        }
    }

}
