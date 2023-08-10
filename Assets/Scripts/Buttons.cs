using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{

    public GameObject UITextObject;

    public GameObject terminalManager;

    

    ShowTerminal showTerminal;

    private void Start()
    {
        showTerminal = FindAnyObjectByType<ShowTerminal>();
    }

    public void ExitButton()
    { 
        
        UITextObject.SetActive(false);
    }

    public void OpenCloseTerminal()
    {

        Debug.Log("Clicking terminal manager button");
        if (terminalManager.activeInHierarchy ==true)
        {
            terminalManager.SetActive(false);
        }
        if(terminalManager.activeInHierarchy == false)
        {
           
        }
    }

    public void debugButtonTest()
    {
        Debug.Log("Button pressed");
    }

    public void ManualButton()
    {

    }


}
