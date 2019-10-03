using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : MonoBehaviour
{
    public GameObject ball;

    private Vector3 distance;

    void Start()
    {
        distance = transform.position - ball.transform.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = ball.transform.position + distance;

    }
}
