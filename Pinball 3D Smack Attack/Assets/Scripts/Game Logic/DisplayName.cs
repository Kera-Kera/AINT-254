using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    [SerializeField]
    private GameObject playFab = null;

    [SerializeField]
    private InputField inputField = null;

    [SerializeField]
    private bool isNameSubmit = false;

    private PlayfabManager playfabScript = null;

    private void Start()
    {
        playfabScript = playFab.GetComponent<PlayfabManager>();

        if (isNameSubmit)
        {
            inputField.text = playfabScript.GetDisplayName();
        }
        

    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            if (isNameSubmit)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    PlayerNameSubmit();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    PlayerEmailSubmit();
                }
            }
            
        }

        if (playfabScript.IsEmailUpdated() == 1)
        {
            gameObject.SetActive(false);
            inputField.text = "";
            playfabScript.ResetEmailUpdated();
        }
        if (playfabScript.IsEmailUpdated() == 2)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            playfabScript.ResetEmailUpdated();
        }

    }


    public void PlayerNameSubmit()
    {
        if (inputField.text.Length > 2)
        {
            playfabScript.UpdateName(inputField.text);


            gameObject.SetActive(false);
        }

        else
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

    }

    public void PlayerEmailSubmit()
    {
        playfabScript.UpdateEmail(inputField.text);
    }

    public void ClosePlayerSubmit()
    {
            gameObject.SetActive(false);
    }

    public void OpenPlayerNameSubmit()
    {
        gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
}
