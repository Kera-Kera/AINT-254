using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            SceneManager.LoadScene(0);
        }
        //This code used to load the last scene, but had no need for it so now its a code that sends the player to the menu
    }
}