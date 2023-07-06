using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject carriedObject;
    // Update is called once per frame
    void Update()
    {
        if (TiltFive.Input.TryGetTrigger(out var triggerValue))
        {
            if (triggerValue < 0.5f && carriedObject != null)
            {
                DropObject();

            }
        }
       

    }
    private void OnTriggerStay(Collider other)
    {
        if (TiltFive.Input.TryGetTrigger(out var triggerValue))
        {
            if (triggerValue > 0.5f && carriedObject == null)
            {
                PickUpObject(other.gameObject);
            }
        }
    }

  

    private void PickUpObject(GameObject obj)
    {
        obj.transform.parent = transform;
        carriedObject = obj;
        carriedObject.GetComponent<Rigidbody>().useGravity = false;
        carriedObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void DropObject()
    {
        carriedObject.transform.parent = null;
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
       carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }
}
