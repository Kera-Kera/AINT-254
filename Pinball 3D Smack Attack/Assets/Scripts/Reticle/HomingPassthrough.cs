using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingPassthrough : MonoBehaviour
{
    [SerializeField]
    private GameObject target = null;
    [SerializeField]
    private float SpawnTimer = 2.0f;

    // Start is called before the first frame update
    public void IsSelected()
    {
        target.gameObject.SetActive(true);
    }

    public void IsNotSelected()
    {
        target.gameObject.SetActive(false);
    }
    public float GetSpawnTime()
    {
        return SpawnTimer;
    }
}
