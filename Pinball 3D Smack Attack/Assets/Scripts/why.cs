using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class why : MonoBehaviour
{

    public GameObject op;
    public GameObject ball;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offSet = new Vector3(0.0f, 5f, 0.0f);
        this.transform.position = ball.transform.position + offSet;
        if (Vector3.Distance(ball.transform.position,op.transform.position) < 5)
        {
            this.transform.LookAt(op.transform);
        }
        else
        {
            this.transform.LookAt(ball.transform);
        }
    }
}
