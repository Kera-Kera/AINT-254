using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJump : MonoBehaviour
{
    public float jumpForce;
    public bool isGrounded;


    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        int notPlayerMask = ~(1 << 8);

        isGrounded = Physics.CheckSphere(transform.position - (Vector3.up * 0.08f), 0.5f, notPlayerMask);

        if (Input.GetButtonDown("Jump"))
        {

            if (isGrounded)
            {
                if (rb.velocity.y > -1 && rb.velocity.y < 1)
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                }
            }
        }
    }
}
  //  IEnumerator DelayedJumping()
 //   {
  //      float timer = 0;
  //      for (; ; )
//              {
//
   //         timer += 1;
   //         if (isGrounded)
   //         {
  //              
 //              if (rb.velocity.y > -8 && rb.velocity.y < 0)
  //              {
  //                  Debug.Log("done" + rb.velocity.y);
  //                  rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
  //                  
    //                StopCoroutine("DelayedJumping");
    //
      //          }
    //        }
//
 //           if (timer == 3)
 //           {
 //               StopCoroutine("DelayedJumping");
  //          }
//
//            yield return new WaitForSeconds(.05f);
//        }
 //   }

