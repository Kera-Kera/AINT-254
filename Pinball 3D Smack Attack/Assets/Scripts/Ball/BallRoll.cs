using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoll : MonoBehaviour
{
    public float speed;
    public Transform camTransform;
    //public speed float so we can adjust the speed without opening 
    //the script editor.

    private Rigidbody rb;
    private bool isIce;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //retrieve the rigidbody from the player GameObject

    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        //uses the inputs that are part of Unity to return a value
        //between -1 and 1, so  pressing W will make the float 1, however
        //pressing S will cause the float to be -1, meaning you can move
        //back and forth

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Vector3 movement = camTransform.TransformDirection(new Vector3(moveVertical, 0.0f, -moveHorizontal));

        rb.AddTorque(movement * speed * Time.deltaTime);

        if (moveHorizontal == 0 && moveVertical == 0) 
        {
            if (isIce == false)

            {
                rb.angularVelocity /= 1.05f;
            }
            else
            {
                rb.angularVelocity /= 1.01f;
            }

        }

        //adds force to the player, causing them to move

            //this is the base code from the Unity Movement Basics tutorial, my own 
            //will be added in the other scripts however this script change also.

            //this.transform.Rotate(movement * speed, 5f, Space.World);

    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.collider.tag == "Ice")
        {
            isIce = true;
        }
        else
        {
            isIce = false;
        }
    }
}
