using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.


/*
 * 
 
 * */
public class InFocusCheckUI : MonoBehaviour
{
	public GameObject mainInputField;

	// class to 
	void Update()
	{
		// If the input field is focused, change its color to the specific hex value #696969.
		if (mainInputField.GetComponent<InputField>().isFocused == true)
		{
			float r = 50f / 255f; // Red
			float g = 50f / 255f; // Green
			float b = 50f / 255f; // Blue

			mainInputField.GetComponent<Image>().color = new Color(r, g, b);
			Debug.Log("Input in focus");
		}
	
	}
}
