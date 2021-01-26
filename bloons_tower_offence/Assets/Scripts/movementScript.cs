using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class movementScript : NetworkBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -5f;
    public float jumpStrength = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float x;
    public float z;
    Vector3 velocity;
    Vector3 move;
    public bool isGrounded;

    // To disable
    public Camera cam;
    


    private void Start()
    {
        if (!isLocalPlayer) {
            cam.enabled = false;
            cam.GetComponent<lookScript>().enabled = false;

            this.enabled = false;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            speed = 4f;
        }
        else
        {
            speed = 12f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //put here if you want to make it only on ground
        if (isGrounded && velocity.y< 0)
        {
            z = Input.GetAxis("Vertical");
            x = Input.GetAxis("Horizontal");
            


            velocity.y = -2f;
        }
        move = transform.right * x + transform.forward * z;

        controller.Move(move* speed* Time.deltaTime);

        if(Input.GetKey("space") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    
}
