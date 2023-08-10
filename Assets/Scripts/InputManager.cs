
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{

    //new input system 
    private TiltFive.WandDevice wandDevice;

    [SerializeField] public GameObject carriedObject;
    public Book book;
    public GameObject bookObject;
    public bool bookOpen;
    GameObject cube;
    GameObject bookUI;

    // trying to use sdk input example 
    [SerializeField]
    private UnityEvent _onTriggerPressed;
    // Update is called once per frame

    // Game objects that wand will interact with 



    public GameObject btnRight;
   public  GameObject btnLeft;

     // materials 
    Material mat1;
    Material mat2;
    Material mat3;

    public int speed;

/*    private void Start()
    {
        book = FindObjectOfType<Book>();
    }
*/
    void Update()
    {

      

            //BookActions();

            

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
      
            if (triggerValue > 0.5f && carriedObject == null)
            {
                PickUpObject(other.gameObject);
                ButtonPress(other.gameObject);
                //PressButton(other.gameObject);
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

    private void ButtonPress(GameObject button)
    {
        if (button.CompareTag("Button"))
        {
            Debug.Log("button pressed");
        }
    }

    public void BookActions()
    {
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
            }

            // Handle stick movement
            cube.transform.Translate(wandDevice.Stick.ReadValue().x * Time.deltaTime * speed, 0.0f, wandDevice.Stick.ReadValue().y * Time.deltaTime * speed);
        }

    }

    /// <summary>
    /// On clicking the UI.
    /// </summary>
    public void OnClick()
    {
        // Check that there's an event system present in the scene.
        if (EventSystem.current != null)
        {
            // Create a pointer event data and execute on the current event system.
            PointerEventData data = new PointerEventData(EventSystem.current);

            data.selectedObject = EventSystem.current.currentSelectedGameObject;

            ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.submitHandler);
        }
    }
}
