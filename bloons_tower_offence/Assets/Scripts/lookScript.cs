using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookScript : MonoBehaviour
{
    /*
    public Transform upAngle;
    public Transform midAngle;
    public Transform downAngle;
    
    public GameObject gun;
    */
    public float mouseSensitivity = 1000f;
    public float defaultMouseSensitivity = 875f;
    public Transform beetleBody;
    float xRot = 0f;
    float fov;
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = gameObject.GetComponent<Camera>();
        Cursor.visible = false;
        fov = 65f;
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mouseSensitivity = defaultMouseSensitivity* 3f;
            fov = 40f;
        }
        else
        {
            mouseSensitivity = defaultMouseSensitivity;
            fov = 65f;
        }


        playerCamera.fieldOfView = fov;

        float mouseX = Input.GetAxis("Mouse X")* mouseSensitivity* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        beetleBody.Rotate(Vector3.up * mouseX);
        

    }
}
