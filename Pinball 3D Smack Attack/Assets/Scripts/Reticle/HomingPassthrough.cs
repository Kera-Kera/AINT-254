using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingPassthrough : MonoBehaviour
{
    [SerializeField]
    private float SpawnTimer = 2.0f;

    // Start is called before the first frame update
    public void IsSelected()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    //This is a public method that enableds the gameObject that is a child of this one (used for the reticle around the targets.)
    public void IsNotSelected()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    //This disables the gameObject that is a child of the gameObject
    public float GetSpawnTime()
    {
        return SpawnTimer;
    }
    //returns the spawn timer to be used when spawning the target in the homing attack code
}
