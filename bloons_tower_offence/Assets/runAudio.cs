using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runAudio : MonoBehaviour
{
    public AudioSource footsteps;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = player.GetComponent<movementScript>().isGrounded;
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) && isGrounded)
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play(0);
            }
        }
        else
        {
            footsteps.Stop();
        }
    }
}
