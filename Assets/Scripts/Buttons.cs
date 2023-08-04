using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{

    public GameObject UITextObject;

    public void ExitButton()
    { 
        UITextObject.SetActive(false);
    }


}
