using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class InFocusCheckUI : MonoBehaviour
{
	public GameObject mainInputField;

	void Update()
	{
		//If the input field is focused, change its color to green.
		if (mainInputField.GetComponent<InputField>().isFocused == true)
		{
			mainInputField.GetComponent<Image>().color = Color.green;
			Debug.Log("Input in focus");
		} else
        {
			Debug.Log("Input not in focus")
        }
	}
}