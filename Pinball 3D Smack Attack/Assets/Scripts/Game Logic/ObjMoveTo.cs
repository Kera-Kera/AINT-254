using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveTo : MonoBehaviour
{
    private Vector3 startingLocal;

    [SerializeField]
    private Transform endingLocal = null;
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private bool isTriggered = true;
    [SerializeField]
    private bool moveBack = true;
    [SerializeField]
    private bool movingFor = true;

    private float moveSpeed2;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed2 = moveSpeed;
        startingLocal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (isTriggered)
        {
            if (!moveBack)
            {
                MoveObj();
            }
            if (moveBack)
            {
                MoveObjBack();
            }
        }
        
    }

    private void MoveObj()
    {
        transform.position = Vector3.MoveTowards(startingLocal, endingLocal.position, moveSpeed * Time.deltaTime);
    }

    private void MoveObjBack()
    {
        if (movingFor)
        {
            transform.position = Vector3.MoveTowards(transform.position, endingLocal.position, moveSpeed2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, startingLocal) < 2f)
            {
                moveSpeed2 *= 1.017f;
            }
            else if(Vector3.Distance(transform.position, endingLocal.position) < 2f)
            {
                moveSpeed2 /= 1.017f;
            }
            else
            {
                moveSpeed2 = moveSpeed;
            }

            if (transform.position == endingLocal.position)
            {
                movingFor = false;
            }
        }
        if (!movingFor)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingLocal, moveSpeed2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, endingLocal.position) < 2f)
            {
                moveSpeed2 *= 1.017f;
            }
            else if (Vector3.Distance(transform.position, startingLocal) < 2f)
            {
                moveSpeed2 /= 1.017f;
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
    }

    public void setTriggered()
    {
        isTriggered = !isTriggered;
    }
}
