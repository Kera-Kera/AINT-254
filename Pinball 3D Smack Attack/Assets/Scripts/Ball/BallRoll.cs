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

    [SerializeField]
    private AudioSource Audio1;
    [SerializeField]
    private AudioSource Audio2;

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

        if (gameObject.GetComponent<BallJump>().isGrounded == true)
        {
            if (rb.velocity.magnitude > 0.8 && transform.position.y > - 5)
            {
                Debug.Log("this1");
                Audio1.volume = (rb.velocity.magnitude / 5) - 0.1f;
                if ((rb.velocity.magnitude / 2) - 0.1f < 1 && (rb.velocity.magnitude / 2) - 0.1f > 0.9)
                {
                    
                    Audio1.pitch = (rb.velocity.magnitude / 2) - 0.1f;
                }
            }
            else
            {
                Debug.Log("this2");
                Audio1.volume = 0;
            }
        }
        else
        {
            Debug.Log("this3");
            Audio1.volume = 0;
        }
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            if (gameObject.GetComponent<BallJump>().isGrounded == true)
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

        if (collision.relativeVelocity.magnitude >= 2 && collision.transform.tag != "Deathzone")
        {
            Audio2.pitch = 1f;
            //Audio1.volume = 0;
            Audio2.volume = (collision.relativeVelocity.magnitude - 2.75f) / 10;
            Audio2.Play();
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

    

    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
