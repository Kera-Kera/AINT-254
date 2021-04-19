using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{

    private List<string> leaderboardTime = new List<string>();

    private List<string> leaderboardName = new List<string>();

    private bool currentNameUpdated = false;

    private string currentName = null;

    private string currentPlayerRecord = null;

    private string currentPlayerPosition = null;

    private int isEmailUpdated = 0;

    private bool isUpdated = false;
    private bool isUpdated2 = false;
    private bool isSent = false;

    // Start is called before the first frame update
    void Start()
    {
        Login();   
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true}
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Login Successful");
        if (result.InfoResultPayload.PlayerProfile.DisplayName != null)
        {
            
            currentName = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        else
        {
            currentName = "";
        }
        currentNameUpdated = true;
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error Logging In or Creating an Account");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int time, string levelName)
    {
        if (levelName != "not assigned")
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = levelName,
                        Value = time
                    }
                }
            };

            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
        }
        else
        {
            Debug.Log("No level data");
        }
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Leaderboard Sent");
        isSent = true;
    }

    public void GetLeaderboard(string currentLevel)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = currentLevel,
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        var request2 = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = currentLevel,
            PlayFabId = null,
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request2, OnLeaderboardAroundPlayerGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            float time = (float)item.StatValue / 100 * -1;
            string timeString = time.ToString();
            if (item.StatValue % 100 == 0)
            {
                timeString += ".00";
            }
            else if (item.StatValue % 10 == 0)
            {
                timeString += "0";
            }

            leaderboardName.Add(item.DisplayName);
            leaderboardTime.Add(timeString);
        }
        isUpdated = true;
        Debug.Log("Leaderboard Recieved");
    }

    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result)
    {
        string timeString = ((float)result.Leaderboard[0].StatValue / 100 * -1).ToString();
        if (result.Leaderboard[0].StatValue % 100 == 0)
        {
            timeString += ".00";
        }
        else if (result.Leaderboard[0].StatValue % 10 == 0)
        {
            timeString += "0";
        }
        currentPlayerRecord = timeString;
        currentPlayerPosition = (result.Leaderboard[0].Position + 1).ToString();
        isUpdated2 = true;
    }

    public void UpdateName(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateName, OnError);
    }

    private void OnUpdateName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display Updated");
        currentName = result.DisplayName;
    }

    public void UpdateEmail(string email)
    {
        var request = new AddOrUpdateContactEmailRequest
        {
            EmailAddress = email
        };
        PlayFabClientAPI.AddOrUpdateContactEmail(request, OnUpdateEmail, OnEmailError);
    }

    void OnEmailError(PlayFabError error)
    {
        isEmailUpdated = 2;
    }
    private void OnUpdateEmail(AddOrUpdateContactEmailResult result)
    {
        isEmailUpdated = 1;
    }

    public string GetDisplayName()
    {
        return currentName;
    }

    public List<string> GetCurrentLeaderboard(int option)
    {
        switch (option)
        {
            case 1:
                return leaderboardName;
            case 2:
                return leaderboardTime;
            default:
                return leaderboardTime;
        }
    }

    public bool IsLeaderboardUpdated()
    {
        return isUpdated && isUpdated2;
    }

    public void LeaderboardIsUpdated()
    {
         isUpdated = false;
         isUpdated2 = false;
    }

    public bool IsSent()
    {
        return isSent;
    }

    public void HasBeenSent()
    {
        isSent = false;
    }

    public bool GetIsNameUpdated()
    {
        return currentNameUpdated;
    }
    public string GetCurrectPlayerRecord()
    {
        return currentPlayerRecord;
    }

    public string GetCurrectPlayerPosition()
    {
        return currentPlayerPosition;
    }

    public int IsEmailUpdated()
    {
        return isEmailUpdated;
    }

    public void ResetEmailUpdated()
    {
        isEmailUpdated = 0;
    }
}
