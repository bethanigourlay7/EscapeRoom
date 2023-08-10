using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject UITextObject;

    public GameObject terminalManager;

    ShowTerminal showTerminal;

    TerminalManager terminalManagerScript;

    private void Start()
    {
        showTerminal = FindAnyObjectByType<ShowTerminal>();

        terminalManagerScript = FindObjectOfType<TerminalManager>();
    }

    /**
    * Script for play button to change to lvl1 scene
    */
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    // displays and hides UI text with information to direct user
    public void HelpButton()
    {
        if (UITextObject.activeInHierarchy)
        {
            UITextObject.SetActive(false);
            return;
        }
        else
        {
            UITextObject.SetActive(true);
            return;
        }
       
    }

    public void OpenCloseTerminal()
    {


        Debug.Log("Clicking terminal manager button");
        if (terminalManager.activeInHierarchy ==true)
        {
            terminalManager.SetActive(false);
            return;
        }
        if(terminalManager.activeInHierarchy == false)
        {
            terminalManager.SetActive(true);
            return;
        }
    }

    public void debugButtonTest()
    {
        Debug.Log("Button pressed");
    }

    public void ManualButton()
    {

    }

    /*
     * Set to easy mode
     */
    public void EasyMode()
    {
        terminalManagerScript.easyLevel = true;
    }

    /*
     * set to hard mode
     */
    public void hardMode()
    {
        terminalManagerScript.easyLevel = false;
    }




}
