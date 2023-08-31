using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowTerminal : MonoBehaviour
{
    Robot robot;
    [SerializeField] private GameObject terminal;
    [SerializeField] private GameObject environment;

    string currentSceneName;

    bool terminalDisplayed;
  

    // Start is called before the first frame update
    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if(currentSceneName == "Level1")
        {
             terminal.SetActive(false);
                    robot = GameObject.FindObjectOfType<Robot>();
        
                    terminalDisplayed = false;
                    DisplayTerminal();
        }
       

    }
    /*
    // Update is called once per frame
    void Update()
    {
        if(terminalDisplayed == false)
        {
             DisplayTerminal();
            terminalDisplayed = true;
        }
       
    }
    */
    /**
     * Displays terminal and hides environment within scene
     */
    public void DisplayTerminal()
    {
            terminal.SetActive(true);
       
    }

}
