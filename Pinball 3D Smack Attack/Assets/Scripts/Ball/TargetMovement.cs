﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    public GameObject ball;

    void Update()
    {

        Vector3 offSet = new Vector3(0.0f, 0.4f, 0.0f);
        transform.position = ball.transform.position + offSet;
    }
    //This makes it so the Target will always be at a certain offset to the ball.
}
