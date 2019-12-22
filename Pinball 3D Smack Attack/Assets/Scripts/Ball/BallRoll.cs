using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRoll : MonoBehaviour
{
    public float speed;
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

        Vector3 movement = Camera.main.transform.TransformDirection(new Vector3(moveVertical, 0.0f, -moveHorizontal));
        //Uses the cameras Transform to control the movement of the player

        rb.AddTorque(movement * speed * Time.deltaTime);
        //Adds Torque(rotation) in the direction of the camera

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            if (this.gameObject.GetComponent<BallJump>().isGrounded == true)
            {
                if (isIce == true)

                {
                    rb.angularVelocity /= 1.05f;
                }

                if (isIce == false)
                {
                    rb.angularVelocity /= 1.01f;
                }
            }
            //I used this for some extra friction to make the ball stop when the player stops moving 
            else
            {
                rb.angularVelocity = rb.angularVelocity;
            }
            //Making sure that in the air it keeps its angularVelocity
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.collider.tag == "Ice")
        {
            isIce = false;
        }
        if (collision.collider.tag != "Ice")
        {
            isIce = true;
        }
    }
    //Checking if the ball is colliding with ice, ended up not using an ice in the submission however will add some later
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.parent.SetParent(col.transform);
        }
    }
    //When the Player collides with a Platform object(one that is moving) it uses this to move with it.
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.parent.SetParent(null);
        }
    }
    //When the player leaves the Platform, it wont get dragged by the transform of the platform.
}
