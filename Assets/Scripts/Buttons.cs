using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject UITextObject;

    public GameObject terminalButton;

    public GameObject terminalManager;

    public GameObject terminalUI;

    public GameObject manualButton;

    public GameObject manual;

    public GameObject book;

    public GameObject tutorialButton;

    // Wand pointer view object needs to be disabled when terminal is opened so mouse can be used
    public GameObject wandPointerView;

    GameManager gameManager;


    TextController textController;

    ShowTerminal showTerminal;

    TerminalManager terminalManagerScript;

    private void Start()
    {
        // showTerminal = FindAnyObjectByType<ShowTerminal>();


        textController = FindObjectOfType<TextController>();
        //terminalManager.SetActive(true);
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

    public void RemoteControl()
    {
        gameManager.remoteControlFound = true;
        Debug.Log("Remote found");
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
                wandPointerView.SetActive(true);
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
           // wandPointerView.SetActive(true);
            Debug.Log("terminal manager set to false");
            return;
        }
        if(terminalManager.activeInHierarchy == false)
        {
            terminalManager.SetActive(true);
            //wandPointerView.SetActive(false);
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
          gameManager.manualFound = true;
            StopAllCoroutines();
            StartCoroutine(textController.DisplayTextOverTime(textController.manualFound));
        }

    }

    public void test()
    {
        Debug.Log("Test button");
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
     * Set to easy mode, used in menu
     */
    public void EasyMode()
    {

        PlayerPrefs.SetInt("EasyMode", 1);
        PlayerPrefs.Save();
    }

    /*
     * set to hard mode, used in menu
     */
    public void hardMode()
    {
        PlayerPrefs.SetInt("EasyMode", 0);
        PlayerPrefs.Save();
    }


    public void TerminalManualToggle()
    {
        if (manual.activeInHierarchy)
        {
            manual.SetActive(false);

        }
    }


    public void OpenCloseBookXButton()
    {
        Debug.Log("X pressed");
        if (book.activeInHierarchy)
        {
            book.SetActive(false);
            terminalManager.SetActive(true);
            terminalUI.SetActive(true);
            return;
        } else if (terminalManager.activeInHierarchy)
        {
            book.SetActive(true);
            terminalManager.SetActive(false);
        }
    }


    public void OpenCloseUITextOverride()
    {
        if (UITextObject.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.D))
            {
                UITextObject.SetActive(false);
                
            }
        }
    }

    public void LoadTerminalScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

}
