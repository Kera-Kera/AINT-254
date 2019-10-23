using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallCamera : MonoBehaviour
{
    public GameObject ball;

    private Vector3 distance;

    void Start()
    {
        distance = transform.position - ball.transform.position;

    }

<<<<<<< Updated upstream
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = ball.transform.position + distance;
=======
    private void Start()
    {
        Screen.SetResolution(1280 , 720, false);
    }

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * 2;
        mouseY -= Input.GetAxis("Mouse Y") * 4;
        mouseY = Mathf.Clamp(mouseY, -40, 85);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }




        Cursor.visible = false;
>>>>>>> Stashed changes

    }
}
