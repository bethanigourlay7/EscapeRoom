
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    //new input system 
    private TiltFive.WandDevice wandDevice;

    [SerializeField] public GameObject carriedObject;
    public Book book;

    GameObject cube;
    GameObject bookUI;
    // Update is called once per frame

   
    void Update()
    {
/*
        // with new input system 
        // Get the wand device only once and store the reference for reuse
        if (TiltFive.Wand.TryGetWandDevice(TiltFive.PlayerIndex.One, TiltFive.ControllerIndex.Right, out wandDevice))
        {
            // Handle trigger input
            if (wandDevice.Trigger.IsPressed())
            {
                Debug.Log("Trigger is hit with new input system as well" + wandDevice.Trigger.value);
                book.GetComponent<AutoFlip>().FlipRightPage();
            }

            // When book is present 
            

*//*
            // Handle button input (One and Two)
            if (wandDevice.One.wasPressedThisFrame)
            {
                cube.GetComponent<MeshRenderer>().material = mat1;
            }
            if (wandDevice.Two.wasPressedThisFrame)
            {
                cube.GetComponent<MeshRenderer>().material = mat2;
            }
            if (!wandDevice.One.isPressed && !wandDevice.Two.isPressed)
            {
                cube.GetComponent<MeshRenderer>().material = mat3;
            }*//*

            // Handle stick movement
            cube.transform.Translate(wandDevice.Stick.ReadValue().x * Time.deltaTime * speed, 0.0f, wandDevice.Stick.ReadValue().y * Time.deltaTime * speed);
        }
*/

        if (TiltFive.Input.TryGetTrigger(out var triggerValue))
        {
          
            if (triggerValue < 0.5f && carriedObject != null)
            {
                DropObject();

            }
        }
  
        else if (triggerValue > 0.5f && carriedObject == null)
        {
           
            Debug.Log(book.Mode);
            if (book != null)
            {
                if (book.Mode == FlipMode.RightToLeft) // Access the flip mode using the Mode property
                  
                    book.DragRightPageToPoint(transform.position);
                else
                    book.DragLeftPageToPoint(transform.position);
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
