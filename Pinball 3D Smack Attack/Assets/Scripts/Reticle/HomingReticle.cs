using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingReticle : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    //When these are active, they look at the camera. I used these on the reticles so they look like UI objects but actually theyre just sprites!
}
 
