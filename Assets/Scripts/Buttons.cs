using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

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

    public GameObject helpButton;

<<<<<<< HEAD
    public GameObject Menu;
=======
    public GameObject menu;
>>>>>>> eb88d88fb7d5cbef70efe555234e99f622aa7cbd

    // Wand pointer view object needs to be disabled when terminal is opened so mouse can be used
    public GameObject wandPointerView;

    GameManager gameManager;

    TextController textController;

    ShowTerminal showTerminal;

    TerminalManager terminalManagerScript;

    private void Start()
    {
        // showTerminal = FindAnyObjectByType<ShowTerminal>();

        textController = UITextObject.GetComponentInChildren<TextController>();
       // textController = FindObjectOfType<TextController>();
        if(textController != null)
        {
            Debug.Log("Text controller present");
        }else
        {
            Debug.Log("Text controller not present");
        }
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

        //HelpButton();
    }


    /**
    * Script for play button to change to lvl1 scene
    */
    public void PlayGame()
    {
<<<<<<< HEAD
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        helpButton.SetActive(true);
        tutorialButton.SetActive(true);
        gameManager.Tutorial();
        Menu.SetActive(false);
=======
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    
        gameManager.Tutorial();

>>>>>>> eb88d88fb7d5cbef70efe555234e99f622aa7cbd
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif      
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
        if (gameManager.manualFound == false)
        {
            StopAllCoroutines();
            StartCoroutine(textController.DisplayTextOverTime(textController.remoteFounf));
        }
    }


    /**
     * Only available if terminal is active. This turns off the raycast so only the keyboard user can type
     * Pressing escape closes the terminal and turns the raycast from the wand back on so tilt five user can
     * interact with the board. 
     */
    void EscapeButton()
    {
        if(terminalManager != null)
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

    /*
     * Not in use, was used to activate the manual button when the manual was found in the scene
     */
    public void FindManualButton()
    {
        if(manualButton.activeInHierarchy == false)
        {
            manualButton.SetActive(true);
           
            Debug.Log("Clicked find manual button");
          gameManager.manualFound = true;
            //UITextObject.SetActive(true);
            if(gameManager.remoteControlFound == false)
            {
                StopAllCoroutines();
                StartCoroutine(textController.DisplayTextOverTime(textController.manualFound));
            }
            
        }

    }

    public void test()
    {
        Debug.Log("Test button");
    }

    /*
     * 
     * Activates and deactivates the manual, not in use but was used to 
     */
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


    public void SwapToManual()
    {
        gameManager.ShiftToManual();
    
    }

    public void FixRobotLoadFreeStyle()
    {
        // variable to pass through
        PlayerPrefs.SetInt("Freestyle", 1);
        
        gameManager.atStageFour = true;
        gameManager.atStageOne = false;
        gameManager.atStageTwo = false; 
       
     
       
    }

   

}
