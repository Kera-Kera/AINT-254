using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingReticle : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
    }
}
 
