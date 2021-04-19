using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{



    [SerializeField]
    private GameObject endOfLevel = null;

    [SerializeField]
    private string levelName = "not assigned";

    [SerializeField]
    private Transform levelText = null;

    [SerializeField]
    private Transform timerText = null;

    [SerializeField]
    private Transform leaderboardText = null;

    [SerializeField]
    private Transform leaderboardNumberText = null;

    [SerializeField]
    private Transform leaderboardTimeText = null;

    [SerializeField]
    private Transform personalHighScoreText = null;

    [SerializeField]
    private Transform personalHighScorePositionText = null;

    [SerializeField]
    private Transform usernameScreen = null;

    [SerializeField]
    private Transform loadingPage = null;

    private float startTime;

    private float currentTime;

    private string levelTimer;

    private GameObject[] deathzones;

    [SerializeField]
    private GameObject[] loadingGifs;

    private PlayfabManager playfab;

    private bool NowUpdated = false;

    private void Start()
    {
        deathzones = GameObject.FindGameObjectsWithTag("Deathzone");

        startTime = Time.time;

        if (transform.parent != null)
        {
            playfab = transform.parent.GetComponent<PlayfabManager>();
        }
        else
        {
            playfab = transform.GetComponent<PlayfabManager>();
        }
    }


    private void Update()
    {
        if (!Cursor.visible && SceneManager.GetActiveScene().buildIndex != 0)
        {
            currentTime = Time.time - startTime;

            string minutes = ((int)currentTime / 60).ToString();
            string seconds = (currentTime % 60).ToString("f2");

            

            if ((int)currentTime / 60 == 0)
            {
                levelTimer = seconds;
            }
            else
            {
                levelTimer = minutes + ":" + seconds;
            }

            timerText.GetComponent<UnityEngine.UI.Text>().text = levelTimer;


        }
        else if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (playfab.IsSent())
            {
                playfab.GetLeaderboard(levelName);
                playfab.HasBeenSent();
            }


            if (playfab.IsLeaderboardUpdated())
            {
                List<string> leaderboardName = playfab.GetCurrentLeaderboard(1);
                List<string> leaderboardTime = playfab.GetCurrentLeaderboard(2);

                for (int i = 0; i < leaderboardName.Count; i++)
                {
                    leaderboardNumberText.GetComponent<UnityEngine.UI.Text>().text += i+1 + "\n";
                    leaderboardText.GetComponent<UnityEngine.UI.Text>().text += leaderboardName[i] + "\n";
                    leaderboardTimeText.GetComponent<UnityEngine.UI.Text>().text += leaderboardTime[i] + "\n";
                }

                personalHighScoreText.GetComponent<UnityEngine.UI.Text>().text = playfab.GetCurrectPlayerRecord();
                personalHighScorePositionText.GetComponent<UnityEngine.UI.Text>().text = "#" + playfab.GetCurrectPlayerPosition();

                loadingGifs[0].SetActive(false);
                loadingGifs[1].SetActive(false);

                playfab.LeaderboardIsUpdated();
            }
        }
        else
        {
            Cursor.visible = true;
            if (playfab.GetDisplayName() == "" && playfab.GetIsNameUpdated() && !NowUpdated)
            {
                loadingPage.gameObject.SetActive(false);
                usernameScreen.GetComponent<DisplayName>().OpenPlayerNameSubmit();
                NowUpdated = true;
            }
            else if (playfab.GetDisplayName() != "" && playfab.GetIsNameUpdated() && !NowUpdated)
            {
                loadingPage.gameObject.SetActive(false);
                NowUpdated = true;
            }
            else
            {
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Cursor.visible)
        {
            if (other.gameObject == endOfLevel)
            {
                Camera.main.GetComponent<BallCamera>().enabled = false;
                Camera.main.transform.SetParent(null);

                for (int i = 0; i < deathzones.Length; i++)
                {
                    deathzones[i].GetComponent<ResetScene>().disabled(true);
                }
                levelText.GetComponent<UnityEngine.UI.Text>().text = levelName + " Complete!";
                transform.parent.GetChild(0).GetChild(0).gameObject.SetActive(true);
                playfab.SendLeaderboard((int)System.Math.Round(100 * currentTime) * -1, levelName);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
        }
    }
    //Uses the Gameobject collision to load the next scene when the player collides with it

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }


    //Gets the current scene number and adds 1 to the buildIndex, meaning it loads the next level

    public void QuitGame()
    {
        Application.Quit();
    }
    //Quits the application

    
}
