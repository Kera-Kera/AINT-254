using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallJump : MonoBehaviour
{
    public float homingAttackSpeed = 40f;
    public float jumpForce;
    public bool isGrounded;
    public Transform camTransform;
    public bool canHomingAttack;
    public GameObject storedHomingTarget = null;

    private GameObject currentStoredHomingTarget = null;
    private bool usingHoming = false;
    private bool homingUsed = false;
    private Rigidbody rb;
 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle;

        int notPlayerMask = ~(1 << 8);


    
            



        isGrounded = Physics.CheckSphere(transform.position - (Vector3.up * 0.08f), 0.5f, notPlayerMask);

        Collider[] objectColliders = Physics.OverlapSphere(transform.position, 7f);

        for (int i = 0; i <= objectColliders.Length - 1; i++)
        {
            GameObject homingTarget = objectColliders[i].gameObject;
            if (homingTarget.tag == "HomingTarget")
            {



                angle = Vector3.Angle(homingTarget.transform.position - transform.position, Camera.main.transform.forward);
                {
                    if (!usingHoming)
                    {
                        if (Mathf.Abs(angle) < 50)
                        {

                            
                            

                            if (storedHomingTarget == null)
                            {
                                storedHomingTarget = homingTarget;
                            }

                            //Physics.Raycast(transform.position, (transform.position - homingTarget.transform.position), out RaycastHit hit, 10f, notPlayerMask);

                         //   Debug.DrawRay(transform.position, (transform.position - homingTarget.transform.position) * 10f, Color.yellow);

                            //if (hit.transform.tag == homingTarget.tag)
                           // {
                          //  Debug.Log(hit.transform.gameObject);

                                if (Vector3.Distance(transform.position, storedHomingTarget.transform.position) > Vector3.Distance(transform.position, homingTarget.transform.position))
                                {

                                    storedHomingTarget.GetComponent<HomingPassthrough>().IsNotSelected();
                                    storedHomingTarget = homingTarget;

                                }
                          // }
                        }
                    }
                }
            }           
        }

        if (storedHomingTarget != null)
        {
            storedHomingTarget.GetComponent<HomingPassthrough>().IsSelected();
            angle = Vector3.Angle(storedHomingTarget.transform.position - transform.position, Camera.main.transform.forward);
            if (Vector3.Distance(transform.position, storedHomingTarget.transform.position) > 7f || (Mathf.Abs(angle) > 50) || usingHoming == false && homingUsed == true)
            {
                storedHomingTarget.GetComponent<HomingPassthrough>().IsNotSelected();
                storedHomingTarget = null;
            }
        }

        if (isGrounded)
        {
            if (rb.velocity.y > -1 && rb.velocity.y < 1 && storedHomingTarget != null)
            {
                storedHomingTarget.GetComponent<HomingPassthrough>().IsNotSelected();
                homingUsed = false;

            }
        }

        if (Input.GetButtonDown("Jump"))
        {

            if (isGrounded)
            {
                if (rb.velocity.y > -1 && rb.velocity.y < 1)
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                    
                }
                homingUsed = false;
            }

            if (!isGrounded)
            {
                if (!homingUsed)
                {
                    if (storedHomingTarget == null)
                    {
                        rb.velocity = camTransform.TransformDirection(Vector3.forward * homingAttackSpeed);
                        homingUsed = true;
                    }
                    else
                    {
                        currentStoredHomingTarget = storedHomingTarget;
                        usingHoming = true;
                        homingUsed = true;
                    }
                }
            }
        }
        if (usingHoming == true && storedHomingTarget != null)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, currentStoredHomingTarget.transform.position, 20f * Time.deltaTime);
            

            if (Vector3.Distance(transform.position, currentStoredHomingTarget.transform.position) < 0.6f)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                StartCoroutine(Spawning(currentStoredHomingTarget));
         
                storedHomingTarget = null;
                homingUsed = false;
                usingHoming = false;
            }
        }
    }

    IEnumerator Spawning(GameObject currentcurrentStoredHomingTarget)
    {

        currentcurrentStoredHomingTarget.SetActive(false);
        yield return StartCoroutine(WaitForSeconds(2.0F));
        currentcurrentStoredHomingTarget.SetActive(true);
    }

    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}

