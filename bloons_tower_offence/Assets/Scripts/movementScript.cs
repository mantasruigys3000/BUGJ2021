using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
           Application.LoadLevel(Application.loadedLevel);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y< 0)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            move = transform.right * x + transform.forward * z;
            velocity.y = -2f;
        }

        

        
        controller.Move(move* speed* Time.deltaTime);

        if(Input.GetKey("space") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
