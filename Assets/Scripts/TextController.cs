using System.Collections;
using UnityEngine;
using TMPro; // Import TextMesh Pro namespace

public class TextController : MonoBehaviour
{
    public TMP_Text uiText; // Use TMP_Text instead of Text
    public float delay = 0.1f;
    private string myString;


    // stages and text
    // start of game
    public string startText = "Hi user, your smart home robot appears to be malfunctioning and is not responding to commands. Find the manual to see instructions on how to fix the robot.";

    // prompt user to find manual
    string findManualText = "There is a manual that may have instructions on how to fix your robot";

    // 

    // robot has been trapped 

    string robotTrapped = "Well done, now the robot has been trapped, see the manual to for next steps";

    // 


    

    void Start()
    {
        myString = uiText.text; // Get the current text in the TMP_Text component
        uiText.text = ""; // Clear the text
    }

    public IEnumerator DisplayTextOverTime(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            uiText.text += text[i];
            yield return new WaitForSeconds(delay);
        }
    }
}
