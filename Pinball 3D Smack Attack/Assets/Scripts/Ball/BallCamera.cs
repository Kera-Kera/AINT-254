using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallCamera : MonoBehaviour
{

    public float distance = 2;
    public Transform Target;
    public float smooth = 0.12f;

    private int notFloorMask = ~(0 << 8);
    [SerializeField]
    private Transform Ball;

    private Vector3 currentRotation;
    private Vector3 smoothvel;
    private Vector3 savedLocation;

    private float mouseX, mouseY;

    private void Start()
    {
        Screen.SetResolution(1280 , 720, false);
    }

    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * 2;
        mouseY -= Input.GetAxis("Mouse Y") * 4;
        mouseY = Mathf.Clamp(mouseY, -35, 85);

        Cursor.visible = false;

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(mouseY, mouseX), ref smoothvel, smooth);
        //this makes the currentRotation smoothly rotate the camera based on the mouseY and mouseX.
        transform.eulerAngles = currentRotation;
        //uses the rotation made in the previous section

        transform.position = Target.position - transform.forward * distance;
        //this uses the forward transform of the camera to always stay looking at the target when the camera is rotating 

        Vector3 RaycastRef = Ball.position;
        CameraRayCast(ref RaycastRef);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        //Pressing escape will send the player back to the main menu
    }

    void CameraRayCast(ref Vector3 TargetFol)
    {
        RaycastHit wallHit;
        if (Physics.Linecast(TargetFol, Camera.main.transform.position, out wallHit))
        {

            transform.position = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
            transform.position = Target.position - transform.forward * (Vector3.Distance(transform.position, Ball.position) * 0.9f);
            transform.LookAt(TargetFol);
        }
    }
    //This method asks for a Vector3 of the player, then whenever something hits the raycast created from the camera to the player, it changes the distance from the player to where the raycast hit.
}
