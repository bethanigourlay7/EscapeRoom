using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject UITextObject;

    public GameObject terminalManager;

    public GameObject manualButton;

    public GameObject book;

    public GameObject tutorialButton;

    GameManager gameManager;

    ShowTerminal showTerminal;

    TerminalManager terminalManagerScript;

    private void Start()
    {
       // showTerminal = FindAnyObjectByType<ShowTerminal>();

        

        terminalManager.SetActive(true);
        terminalManagerScript = FindObjectOfType<TerminalManager>();

       

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        EscapeButton();
    }


    public void StartTutorial()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        gameManager.atTutorial = false;
        Debug.Log("Start tutorial button");
        

    }


    public void ExitTutorial()
    {
        gameManager.atTutorial = false;
        tutorialButton.SetActive(false);

        HelpButton();
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


    /**
     * Only available if terminal is active. This turns off the raycast so only the keyboard user can type
     * Pressing escape closes the terminal and turns the raycast from the wand back on so tilt five user can
     * interact with the board. 
     */
    void EscapeButton()
    {
        if (terminalManager.activeInHierarchy == true)
        {
            if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
               terminalManager.SetActive(false);
            Debug.Log("terminal manager set to false");
            return;
        }
           
        }
    }
    public void OpenCloseTerminal()
    {


        Debug.Log("Clicking terminal manager button");
        if (terminalManager.activeInHierarchy ==true)
        {
            terminalManager.SetActive(false);
            Debug.Log("terminal manager set to false");
            return;
        }
        if(terminalManager.activeInHierarchy == false)
        {
            terminalManager.SetActive(true);
            foreach (Transform child in terminalManager.transform)
            {

                child.gameObject.SetActive(true);
            }
            return;
        }
    }


    public void FindManualButton()
    {
        if(manualButton.activeInHierarchy == false)
        {
            manualButton.SetActive(true);
            Debug.Log("Clicked find manual button");
        }

    }

    public void ManualButton()
    {
        Debug.Log("Manual button pressed");
        if(book.activeInHierarchy == true)
        {
            book.SetActive(false);
            return;
        } else
        {
            book.SetActive(true);
        }
    }

    /*
     * Set to easy mode
     */
    public void EasyMode()
    {

        PlayerPrefs.SetInt("EasyMode", 1);
        PlayerPrefs.Save();
    }

    /*
     * set to hard mode
     */
    public void hardMode()
    {
        PlayerPrefs.SetInt("EasyMode", 0);
        PlayerPrefs.Save();
    }




}
