using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject bookCase;
    private Rigidbody bookCaseRb;

    void Start()
    {
        // Check if the bookCase has been assigned in the inspector
        if (bookCase == null)
        {
            Debug.LogError("bookCase not assigned in the inspector!");
            return;
        }

        // Get the Rigidbody component from the bookCase
        bookCaseRb = bookCase.GetComponent<Rigidbody>();

        if (bookCaseRb == null)
        {
            Debug.LogError("No Rigidbody component found on the bookCase GameObject.");
        }

        FreezeBookCase();
    }

    // Method to "freeze" the bookcase
    public void FreezeBookCase()
    {
        if (bookCaseRb != null)
        {
            bookCaseRb.isKinematic = true;
        }
    }

    // Method to "unfreeze" the bookcase
    public void UnfreezeBookCase()
    {
        if (bookCaseRb != null)
        {
            bookCaseRb.isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
