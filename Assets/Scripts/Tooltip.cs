using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tooltip : MonoBehaviour
{

    private TMP_Text textMeshPro;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        setText("Hello");
    } 
    /*
     * Display prompt for user
     */
    private void ShowToolTip(string toolTipString)
    {
        Debug.Log("Tooltip string");
        gameObject.SetActive(true);
        textMeshPro.text = toolTipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(textMeshPro.preferredWidth + textPaddingSize * 2f, textMeshPro.preferredHeight + textPaddingSize * 2f);
        //Dynamically set size of tool tip depending on text
        backgroundRectTransform.sizeDelta = backgroundSize;
    
    }

    /*
     * 
     * Hide prompt from user
     */
    private void HideToolTip()
    {
        gameObject.SetActive(false);
    }

    private void setText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        backgroundRectTransform.sizeDelta = textSize * 2f;
    }
}
