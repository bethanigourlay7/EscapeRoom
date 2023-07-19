
using UnityEngine;

public class PickUpCollision : MonoBehaviour
{

     public bool collidedWithPickup = false;

    public void Update()
    {
        if (collidedWithPickup)
        {
            // Handle collision with pickup object here
            Debug.Log("Robot collided with pickup");
            collidedWithPickup = false; // Reset the flag
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collidedWithPickup = true;
           
        }
    }
}
