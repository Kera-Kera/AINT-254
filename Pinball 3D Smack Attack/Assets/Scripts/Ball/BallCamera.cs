using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : MonoBehaviour
{
    public GameObject ball;
    private Vector3 distance;
    public Transform Target;

    private float mouseX, mouseY;

    void Start()
    {
        //distance = transform.position - ball.transform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * 1;
        mouseY -= Input.GetAxis("Mouse Y") * 1;
        mouseY = Mathf.Clamp(mouseY, -90, 90);

        transform.LookAt(Target);


        Target.rotation = Quaternion.Euler(mouseY , mouseX, 0);


        //transform.position = ball.transform.position + distance;
    }

}
