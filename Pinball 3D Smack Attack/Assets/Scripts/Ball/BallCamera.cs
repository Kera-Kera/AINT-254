using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallCamera : MonoBehaviour
{
    public GameObject ball;
    public float distance = 2;
    public Transform Target;
    public float rotationSmoothTime = 0.12f;


    Vector3 currentRotation;
    Vector3 rotationSmoothVelocity;

    private float mouseX, mouseY;

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

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(mouseY, mouseX), ref rotationSmoothVelocity, rotationSmoothTime);

        transform.eulerAngles = currentRotation;

        transform.position = Target.position - transform.forward * distance;
    }

}
