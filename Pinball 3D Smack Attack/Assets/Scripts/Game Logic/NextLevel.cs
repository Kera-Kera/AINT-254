using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            LoadNextLevel();
        }
    }
    //Uses the Gameobject collision to load the next scene when the player collides with it

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 

    //Gets the current scene number and adds 1 to the buildIndex, meaning it loads the next level

    public void QuitGame()
    {
        Application.Quit();
    }
    //Quits the application 
}
