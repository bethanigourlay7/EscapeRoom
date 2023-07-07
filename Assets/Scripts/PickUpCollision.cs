
using UnityEngine;

public class PickUpCollision : MonoBehaviour
{

    private bool collidedWithPickup = false;

    private void Update()
    {
        if (collidedWithPickup)
        {
            // Handle collision with pickup object here
            Debug.Log("Robot collided with pickup");
            collidedWithPickup = false; // Reset the flag
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collidedWithPickup = true;
           
        }
    }
}
