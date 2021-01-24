using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunAngleScript : MonoBehaviour
{

    public Transform leftAngle;
    public Transform centreAngle;
    public Transform rightAngle;
    public GameObject gun;
    public Camera cam;
    public GameObject player;

    void Update()
    {

        if (player.GetComponent<movementScript>().isGrounded)
        {
            if (Input.GetKey(KeyCode.D))
            {
                gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, rightAngle.transform.rotation, (1f * Time.deltaTime) * 4);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, leftAngle.transform.rotation, (1f * Time.deltaTime) * 4);
            }
            else
            {
                gun.transform.rotation = Quaternion.Lerp(gun.transform.rotation, centreAngle.transform.rotation, (1f * Time.deltaTime) * 4);
            }
        }
    }
}
