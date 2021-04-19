using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    private bool isDisabled;
    [SerializeField]
    private GameObject player = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!isDisabled)
        {
            if (other.gameObject == player)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            //This resets the scene when the player colliders with the gameObject. Used as invisible killzones
        }    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    internal void disabled(bool input)
    {
        isDisabled = input;
    }
}
