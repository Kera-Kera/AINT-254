using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingPassthrough : MonoBehaviour
{

    public GameObject target;


    // Start is called before the first frame update
    public void IsSelected()
    {
        target.gameObject.SetActive(true);
    }

    public void IsNotSelected()
    {
        target.gameObject.SetActive(false);
    }
}
