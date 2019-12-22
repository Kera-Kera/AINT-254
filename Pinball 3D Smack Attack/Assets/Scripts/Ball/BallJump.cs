using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool groundPounding = false;
    private Rigidbody rb;
    private ParticleSystem ps;
    private float angle;
    private int notPlayerMask = ~(1 << 8);

    [SerializeField]
    private GameObject ParticleHolder;
    [SerializeField]
    private Text cg;

    private Vector3 camFor;
    private Vector3 lockedForward;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Making it so I can call the rigidbody easier
        ps = ParticleHolder.GetComponent<ParticleSystem>();
        //Making it so I can call the ParticleSystem easier
        StartCoroutine(UIDelay());
        //I start this at the beginning to set a timer so the text will disappear after 5 seconds of being on screen
    }

    // Update is called once per frame
    void Update()
    {

        FindTarget();

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
        //This checks if storedHomingTarget isnt equal to nothing, if so then it turns on the reticle attached to the Target
        //It then checks for 3 different things, If the distance between the player and the target is over 7 units, 
        //if the angle of the camera is 50 degrees away,
        //and if usingHoming and homingUsed is true.
        //If one of these is true, then the storedHomingAttack is made to equal nothing and the reticle is turned off
        if (isGrounded)
        {
            if (rb.velocity.y > -1 && rb.velocity.y < 1 && storedHomingTarget != null)
            {
                storedHomingTarget.GetComponent<HomingPassthrough>().IsNotSelected();
                homingUsed = false;

            }
        }
        //If grounded it removes the reticle and makes homingUsed false, so that the next time the player is in the air he can homing attack.

        if (Input.GetButtonDown("Jump"))
        {
            BallJumping();
        }

        if (Input.GetKeyDown(KeyCode.E) && !isGrounded)
        {
            rb.velocity = new Vector3(0, -20, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);
            groundPounding = true;            
        }
        //This code makes it so that when the player isnt grounded they can press E to remove all velocity and go straight down

        if (groundPounding && !isGrounded)
        {
            rb.angularVelocity = new Vector3(0, 0, 0);
        }
        //Whilst you are groundpounding there is no rotation

        if (groundPounding && isGrounded)
        {
            ps.Play();
            rb.velocity = new Vector3(0, 0, 0);
            groundPounding = false;
            StartCoroutine(Particle());
        }
        //once you hit the ground, make the velocity reset to 0 and play particle effect

        if (usingHoming == true && currentStoredHomingTarget != null)
        {
            HomingCheck();
        }
        //if there is a Homing Attack target that is currently being attacked, this code happens.

    }

    private void FindTarget()
    {
        isGrounded = Physics.CheckSphere(transform.position - (Vector3.up * 0.08f), 0.45f, notPlayerMask);
        //Checks if the player is on the ground by making a Sphere to check if anything hits it, its slightly beneath the player so if it collides with something it i will be the floor
        Collider[] objectColliders = Physics.OverlapSphere(transform.position, 7f);
        //this stores all the colliding gameObjects that collide with the Sphere created. This is meant to looking for targets to attack
        for (int i = 0; i <= objectColliders.Length - 1; i++)
        {
            GameObject homingTarget = objectColliders[i].gameObject;
            if (homingTarget.tag == "HomingTarget")
            //The code goes through all the things its collidering with and checks for ones with the HomingTarget tag
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
                            if (Vector3.Distance(transform.position, storedHomingTarget.transform.position) > Vector3.Distance(transform.position, homingTarget.transform.position))
                            {
                                storedHomingTarget.GetComponent<HomingPassthrough>().IsNotSelected();
                                storedHomingTarget = homingTarget;
                            }
                            //if the player is not currently homing towards a target, and the target is within a 50 degree angle to the camera then:
                            //if there is no current stored homing target then make it the storedHomingTarget
                            //however if there is a storedHomingTarget, then make the gameObject which is closer the storedHomingTarget
                        }
                    }
                }
            }
        }
    }
    private void BallJumping()
    {
        if (isGrounded)
        {
            if (rb.velocity.y > -3 && rb.velocity.y < 3)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

            }
            homingUsed = false;
        }
        //if the player is on the ground then make the player jump into the air.

        if (!isGrounded)
        {
            if (!homingUsed)
            {
                if (storedHomingTarget == null)
                {
                    Vector3 camDirection = Camera.main.transform.forward;
                    camDirection.y = 0;
                    Vector3 camCross = Vector3.Cross(camDirection, Vector3.up);
                    Vector3 dashLocal = Quaternion.AngleAxis(20, camCross) * camDirection;
                    rb.velocity = dashLocal.normalized * homingAttackSpeed ;
                    Vector3 movement = Camera.main.transform.TransformDirection(new Vector3(100, 0.0f, 0));
                    rb.AddTorque(movement * 250f * Time.deltaTime);
                    homingUsed = true;
                }
                //if the player is not grounded, and if there is no storedHomingTarget, then the player will be launched where the camera is facing, but it will always have the same Y value for the velocity.
                //this was done by finding the forward of the camera, making the whatever the Y value was 0, then adding an angle to the flatterned Y value in the direction the camera is facing
                //It then puts that value into the velocity of the player, rotating in the way the player was launched too.
                else
                {
                    currentStoredHomingTarget = storedHomingTarget;
                    usingHoming = true;
                    homingUsed = true;
                }
                //if there is a storedHomingTarget, then it becomes a CurrentStoredHomingTarget, meaning it is currently being used. This makes usingHoming true
                //meaning it triggers the code "HomingCheck" in the update function
            }
        }
    }
    private void HomingCheck()
    {
        rb.velocity = Vector3.zero;
        Vector3 movement = Camera.main.transform.TransformDirection(new Vector3(100, 0.0f, 0));
        rb.AddTorque(movement * 250f * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, currentStoredHomingTarget.transform.position, 20f * Time.deltaTime);

        //When this method gets called, it makes the player move towards the currentStoredHomingTarget 

        if (Vector3.Distance(transform.position, currentStoredHomingTarget.transform.position) < 0.6f)
        {
            Vector3 camDirection = Camera.main.transform.forward;
            camDirection.y = jumpForce;
            Vector3 camCross = Vector3.Cross(camDirection, Vector3.up);
            Vector3 dashLocal = Quaternion.AngleAxis(-20, camCross) * camDirection;
            rb.velocity = dashLocal.normalized * jumpForce;
            StartCoroutine(Spawning(currentStoredHomingTarget));

            storedHomingTarget = null;
            homingUsed = false;
            usingHoming = false;
        }
        //When the player gets 0.6f units away from the target, it launches the ball up with that same force as a jump, as well as "destorying" the target
        //This makes it look like the player has hit the target and caused it to disappear. Thhis uses the same code as the untargeted homing attack, so theres a bit of an angle when you hit off a target
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "HomingTarget")
        {
            if (usingHoming)
            {
                usingHoming = false;
                rb.AddExplosionForce(500f, collision.transform.position, 500f);
            }
        }
    }
    //if you get hit during a homing attack it will knock you off course

    IEnumerator Spawning(GameObject SavedHomingTarget)
    {
        SavedHomingTarget.SetActive(false);
        yield return StartCoroutine(WaitForSeconds(SavedHomingTarget.GetComponent<HomingPassthrough>().GetSpawnTime()));
        SavedHomingTarget.SetActive(true);
    }
    //uses the wait time to spawn the targets that have been destroyed by the player (gets the spawn time from the target itself
    IEnumerator Particle()
    {
        yield return StartCoroutine(WaitForSeconds(0.1f));
        ps.Stop();
    }
    //Plays the particle for 0.1 of a second
    IEnumerator UIDelay()
    {
        yield return StartCoroutine(WaitForSeconds(5f));
        cg.enabled = false;
    }
    //Disables the UI if the text at the start of each level
    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    //Code used to wait time, used in other Coroutines

}

