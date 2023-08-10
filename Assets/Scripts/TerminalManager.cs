using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerminalManager : MonoBehaviour
{

    public GameObject directoryLine;
    public GameObject responseLine;

    public InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect sr;
    public GameObject msgList;

    // reference to Interpreter for access to list
    Interpreter interpreter;

    // extra interpreter option for easy level
    public bool easyLevel = false;

    private void Start()
    {

        interpreter = GetComponent<Interpreter>();


    }

    private void OnGUI()
    {
        if (terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            // Store what the user types in terminal
            string userInput = terminalInput.text;

            // Clear the input field
            ClearInputField();

            // Instantiate gameobject with a directory prefix
            AddDirectoryLine(userInput);

            int lines;
            // added in a reference to the easy level interpreter if hard
            if(easyLevel == false)
            {
                lines = AddInterpreterLines(interpreter.Interpret(userInput));
            } else
            {
                lines = AddInterpreterLines(interpreter.InterpretEasy(userInput));
            }
            

            // scroll to bottom of the scrollrect
            ScrollToBottom(lines);

            // Move the user input line to the end
            userInputLine.transform.SetAsLastSibling();

            // refocus the input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }
    }

    void ClearInputField()
    {
        terminalInput.text = "";
    }

    void AddDirectoryLine(string userInput)
    {
        // resizing command line container for scrollrect
        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 35.0f);

        // Instantiate the directoru line
        GameObject msg = Instantiate(directoryLine, msgList.transform);

        // set its child index
        msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);

        // Set the text of this new gameobject (remember indexes of children objects)
        msg.GetComponentsInChildren<Text>()[1].text = userInput;

    } 

    int AddInterpreterLines(List<string> interpretation)
    {
        for(int i = 0; i < interpretation.Count; i++)
        {
            // Instantiate respionse line
            GameObject res = Instantiate(responseLine, msgList.transform);

            // set to end of all messages
            res.transform.SetAsLastSibling();

            // Get size of the message list
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 35.0f);

            // set text of this response to the interpreter string
            res.GetComponentInChildren<Text>().text = interpretation[i];

        }

        return interpretation.Count;

    }

    void ScrollToBottom(int lines)
    {
        if(lines > 10)
        {
            Debug.Log("Lines are :" + lines); 
            sr.velocity = new Vector2(0, 450);
        }
        else
        {
            sr.verticalNormalizedPosition = 10; 
        }

    }
}
