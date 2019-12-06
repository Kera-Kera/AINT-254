using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallCamera : MonoBehaviour
{

    public float distance = 2;
    public Transform Target;
    public float rotationSmoothTime = 0.12f;

    private int notFloorMask = ~(0 << 8);
    [SerializeField]
    private Transform Ball;

    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;
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


        Vector3 RaycastRef = Ball.position;

        CameraRayCast(ref RaycastRef);
    }

    void CameraRayCast(ref Vector3 TargetFol)
    {
        RaycastHit wallHit = new RaycastHit();

        if (Physics.Linecast(TargetFol, Camera.main.transform.position, out wallHit))
        {

            transform.position = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
            transform.position = Target.position - transform.forward * (Vector3.Distance(transform.position, Ball.position) * 0.9f);
            transform.LookAt(TargetFol);
        }
    }
}
