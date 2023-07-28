using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{
    public GameObject cube;
    public Material mat1;
    public Material mat2;
    public Material mat3;
    public float speed;

    public GameObject btnRight;
    public GameObject btnLeft;

    public GameObject book;

    // Private variable to hold the wand device reference
    private TiltFive.WandDevice wandDevice;

    void Update()
    {
        // Get the wand device only once and store the reference for reuse
        if (TiltFive.Wand.TryGetWandDevice(TiltFive.PlayerIndex.One, TiltFive.ControllerIndex.Right, out wandDevice))
        {
            // Handle trigger input
            if (wandDevice.Trigger.IsPressed())
            {
                Debug.Log("Trigger is hit with new input system as well" + wandDevice.Trigger.value);
                book.GetComponent<AutoFlip>().FlipRightPage();
            }

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

    private void OnTriggerStay(Collider other)
    {
        // Check if the wand device is available and the trigger is pressed during the collision
        if (wandDevice != null && wandDevice.Trigger.IsPressed())
        {
            Debug.Log("On trigger enter collision");
        }

        Debug.Log("Contact with collider");
    }
}
