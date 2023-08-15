using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFreestyle : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject  robot;
     public float moveSpeed;
    Animator animator;
    Rigidbody rigidBody;

    private void Start()
    {
        animator = robot.GetComponent<Animator>();
        rigidBody = robot.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Freestyle();
    }

    public void Freestyle()
    {
        if (TiltFive.Input.TryGetStickTilt(out var joyStickValue))
        {
            
            robot.transform.Translate(new Vector3(joyStickValue.x, 0, joyStickValue.y) * moveSpeed * Time.deltaTime);
            animator.SetBool("Walk_Anim", true);
            if (rigidBody.velocity != Vector3.zero)
            {
                animator.SetBool("Walk_Anim", false);
            }
        }
    }
}
