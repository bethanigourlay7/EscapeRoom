
using UnityEngine;
using UnityEngine.InputSystem;

public class StageOneInput : MonoBehaviour
{

    //new input system 
    private TiltFive.WandDevice wandDevice;

    [SerializeField] public GameObject carriedObject;

    public int speed;

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
        Debug.Log("on trigger stay method");
        if (TiltFive.Input.TryGetTrigger(out var triggerValue))
        {
            Debug.Log("trigger value " + (float)triggerValue);
            if (triggerValue > 0.5f && carriedObject == null)
            {
                PickUpObject(other.gameObject);
            }


        }
    }


    private void PickUpObject(GameObject obj)
    {
        if (obj.CompareTag("PickUp"))
        {
            obj.transform.parent = transform;
            carriedObject = obj;
            carriedObject.GetComponent<Rigidbody>().useGravity = false;
            carriedObject.GetComponent<Rigidbody>().isKinematic = true;

        }
    }

    private void DropObject()
    {
        carriedObject.transform.parent = null;
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObject = null;
    }
}
