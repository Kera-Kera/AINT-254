using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveTo : MonoBehaviour
{
    private Vector3 startingLocal;

    [SerializeField]
    private Transform endingLocal = null;
    [SerializeField]
    private bool isTriggered = true;
    private bool movingFor = true;

    [SerializeField]
    private float slowDistance = 2;
    [SerializeField]
    private float moveSpeed = 5;

    [SerializeField]
    private float moveSpeed2;

    // Start is called before the first frame update
    void Start()
    {
        startingLocal = transform.position;
        //starting location = the position the object was when the code started
        moveSpeed2 = moveSpeed;
        //this makes it so that moveSpeed2 equals the same as moveSpeed, this is important for the MoveObj method
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            MoveObj();
        }
        //If the script is triggered then do the MoveObj method
    }

    private void MoveObj()
    {
        if (movingFor)
        {
            transform.position = Vector3.MoveTowards(transform.position, endingLocal.position, moveSpeed2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, startingLocal) < slowDistance)
            {

                if (Vector3.Distance(transform.position, startingLocal) < slowDistance / 2)
                {
                    moveSpeed2 = moveSpeed / 4;
                }
                else
                {
                    moveSpeed2 = moveSpeed / 2;
                }
            }
            else if(Vector3.Distance(transform.position, endingLocal.position) < slowDistance)
            {
                if (Vector3.Distance(transform.position, endingLocal.position) < slowDistance / 2)
                {
                    moveSpeed2 = moveSpeed / 4;
                }
                else
                {
                    moveSpeed2 = moveSpeed / 2;
                }

            }
            else
            {
                moveSpeed2 = moveSpeed;
                //once the object moves out of the slowDistance, it returns to its normal speed
            }

            if (transform.position == endingLocal.position)
            {
                movingFor = false;
            }
        }

        //Moving Forward is true, then the object will use MoveTowards to move to the targets location.
        //Once it gets close to the target, it will start to diminish and slow down.
        //However when the object is close to the starting location it has to speed up again as the speed will be very slow since it was just diminished 

        if (!movingFor)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingLocal, moveSpeed2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, endingLocal.position) < slowDistance)
            {

                if (Vector3.Distance(transform.position, endingLocal.position) < slowDistance / 2)
                {
                    moveSpeed2 = moveSpeed / 4;
                }
                else
                {
                    moveSpeed2 = moveSpeed / 2;
                }

            }
            else if (Vector3.Distance(transform.position, startingLocal) < slowDistance)
            {
                if (Vector3.Distance(transform.position, startingLocal) < slowDistance / 2)
                {
                    moveSpeed2 = moveSpeed / 4;
                }
                else
                {
                    moveSpeed2 = moveSpeed / 2;
                }
            }
            else
            {
                moveSpeed2 = moveSpeed;
            }
            if (transform.position == startingLocal)
            {
                movingFor = true;
            }
        }

        //This is the same as the previous code however reversed so it can go back and forward using the movingFor boolean
    }

    public void setTriggered()
    {
        isTriggered = !isTriggered;
    }
    //This can be referenced by other objects, however I didnt get to use it, I will be updating this game after the module with more features and levels where this will be used.
}
