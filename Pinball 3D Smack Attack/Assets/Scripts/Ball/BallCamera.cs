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
    private LayerMask CamMaskFloor;
    [SerializeField]
    private Transform Ball;

    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;

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

        Vector3 RaycastRef = Ball.position;

        CameraRayCast(ref RaycastRef);
    }

    void CameraRayCast(ref Vector3 TargetFol)
    {
        RaycastHit wallHit = new RaycastHit();

        if(Physics.Linecast(TargetFol, Camera.main.transform.position, out wallHit, notFloorMask))
        {
            transform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, transform.position.y, wallHit.point.z + wallHit.normal.z * 0.5f);
            transform.LookAt(TargetFol);
        }
        if (Physics.Linecast(TargetFol, Camera.main.transform.position, out wallHit, CamMaskFloor))
        {
            transform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, transform.position.y + 1f, wallHit.point.z + wallHit.normal.z * 0.5f);
            transform.LookAt(TargetFol);
        }
    }

}
